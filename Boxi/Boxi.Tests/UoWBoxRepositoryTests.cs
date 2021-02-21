using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boxi.Core.Domain;
using Boxi.Dal;
using Boxi.Dal.Interfaces;
using Boxi.Dal.Models;
using Xunit;

namespace Boxi.Tests
{
    public class UoWBoxRepositoryTests : SqliteInMemoryDataContext
    {
        private readonly IUnitOfWork _unitOfWork;
        public UoWBoxRepositoryTests()
        {
            _unitOfWork = new UnitOfWork(new BoxiDataContext(ContextOptions));
        }

        protected override List<Box> DefineBoxes()
        {
            var boxData = new List<Box>();
            for (var i = 1; i <= 3; i++) 
            {
                boxData.Add(new Box($"Box {i}", $"Notes for Box {i}")
                {Items = new List<Item>()
                {
                    new Item($"Box {i}, Item {i}"),
                    new Item($"Box {i}, Item {i + 1}")                        
                }});
            }

            return boxData;
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetById_ShouldReturnFirstBox(int boxId)
        {
            var i = await _unitOfWork.BoxRepo.GetAsync(boxId);
            Assert.Equal(boxId, i.Id);
        }

        [Fact]
        public async Task GetAllAsync_Returns_3Items()
        {
            var results = await _unitOfWork.BoxRepo.GetAllAsync();
            Assert.Equal(3, results.Count());
        }

        [Fact]
        public async Task FetchNextBoxId_Returns_NewestIdPlusOne()
        {
            Assert.Equal(4, await _unitOfWork.BoxRepo.FetchNextBoxIdAsync());
        }

        [Fact]
        public async Task AddAsync_Box_SuccesfulCreationOf_1Box()
        {
            await _unitOfWork.BoxRepo.AddAsync(new Box(nameof(AddAsync_Box_SuccesfulCreationOf_1Box), "notes"));
            var added = await _unitOfWork.SaveAsync();
            Assert.Equal(1, added);

            var box = await _unitOfWork.BoxRepo.FetchAsync(b => b.BoxName == nameof(AddAsync_Box_SuccesfulCreationOf_1Box));
            Assert.NotNull(box);
            Assert.Equal("notes", box.Notes);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task AddAsync_Box_Fetch_BySpecificBoxName(int boxId)
        {
            var box = await _unitOfWork.BoxRepo.FetchAsync(x => x.BoxName.Equals($"Box {boxId}"));
            Assert.NotNull(box);
            Assert.Equal($"Box {boxId}", box.BoxName);
            Assert.Equal($"Notes for Box {boxId}", box.Notes);
        }
    }
}