using Boxi.Core.Commands;
using Boxi.Core.Domain;

namespace Boxi.Core
{
    public static class Map
    {
        public static Box ToEntity(this CreateBoxCommand command)
        {
            return new Box
            {
                BoxName = command.Name,
                Notes = command.Description
            };
        }
    }
}