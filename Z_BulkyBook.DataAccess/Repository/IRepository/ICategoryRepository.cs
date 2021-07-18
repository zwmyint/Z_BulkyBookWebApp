using Z_BulkyBook.Models;

namespace Z_BulkyBook.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        // 
        
        void Update(Category category);

        //
    }
}
