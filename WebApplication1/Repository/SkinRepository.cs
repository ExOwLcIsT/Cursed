using Cursed.Context;
using Cursed.Repository.Interfaces;
using Cursed.Models;

namespace Cursed.Repository
{
    public class SkinRepository : BaseRepository<Skin>
    {
        public SkinRepository(GameContext context) : base(context)
        {
        }
    }
}
