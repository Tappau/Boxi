using System.Linq;
using System.Threading.Tasks;
using Boxi.Core.Domain;
using Boxi.Tests.TestHelpers;
using Boxi.Tests.xUnitExtensions;
using Xunit;

namespace Boxi.Tests
{
    [CollectionDefinition("Non-Parallel Collection", DisableParallelization = true)]
    [TestCaseOrderer("Boxi.Tests.xUnitExtensions.PriorityOrderer", "Boxi.Tests")]
    public class UoWBoxRepositoryTests : IClassFixture<SqliteInMemoryDataContextFixture>
    {
        private readonly SqliteInMemoryDataContextFixture _fixture;

        public UoWBoxRepositoryTests(SqliteInMemoryDataContextFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetById_ShouldReturnFirstBox(int boxId)
        {
            var i = await _fixture.UoW.BoxRepo.GetAsync(boxId);
            Assert.Equal(boxId, i.Id);
        }

        [Fact]
        public async Task GetAllAsync_Returns_3Items()
        {
            var results = await _fixture.UoW.BoxRepo.GetAllAsync();
            Assert.Equal(3, results.Count());
        }

        [Fact]
        public async Task FetchNextBoxId_Returns_NewestIdPlusOne()
        {
            Assert.Equal(4, await _fixture.UoW.BoxRepo.FetchNextBoxIdAsync());
        }

        [Fact]
        public async Task FetchAllAsync_Predicate_Returns_Boxes_WithItemsContainingBarCode()
        {
            var t = await _fixture.UoW.BoxRepo.FetchAllAsync(x => x.Items.Any(i => i.Barcode == "BARCODE"));
            Assert.Equal(2, t.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task AddAsync_Box_Fetch_BySpecificBoxName(int boxId)
        {
            var box = await _fixture.UoW.BoxRepo.FetchAsync(x => x.BoxName.Equals($"Box {boxId}"));
            Assert.NotNull(box);
            Assert.Equal($"Box {boxId}", box.BoxName);
            Assert.Equal($"Notes for Box {boxId}", box.Notes);
        }

        [Fact, Priority(1)]
        public async Task AddAsync_Box_SuccesfulCreationOf_1Box()
        {
            await _fixture.UoW.BoxRepo.AddAsync(new Box(nameof(AddAsync_Box_SuccesfulCreationOf_1Box), "notes"));
            var added = await _fixture.UoW.SaveAsync();
            Assert.Equal(1, added);
        }

        [Theory, Priority(2)]
        [InlineData(nameof(AddAsync_Box_SuccesfulCreationOf_1Box))]
        public async Task FetchAsync_Returns_RecentlyCreatedBox(string boxName)
        {
            var box = await _fixture.UoW.BoxRepo.FetchAsync(b => b.BoxName == boxName);
            Assert.NotNull(box);
            Assert.Equal(boxName, box.BoxName);
            Assert.Equal("notes", box.Notes);
        }

        [Fact, Priority(3)]
        public async Task Delete_Given_BoxId_CreatedInPriority1_ThenCountShouldBe_3()
        {
            _fixture.UoW.BoxRepo.Delete(4);
            await _fixture.UoW.SaveAsync();
            Assert.Equal(3, await _fixture.UoW.BoxRepo.TotalCountAsync());
        }

        [Fact, Priority(4)]
        public async Task TotalCountAsync_Returns_3()
        {
            Assert.Equal(3, await _fixture.UoW.BoxRepo.TotalCountAsync());
        }
    }
}
