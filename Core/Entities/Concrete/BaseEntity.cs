using Core.Entities.Abstract;

namespace Core.Entities.Concrete
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
