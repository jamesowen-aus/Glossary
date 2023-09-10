using Glossary.Web.Api.Modules.Glossary.Dtos;
using Glossary.Web.Api.Modules.Glossary.Model;

namespace Glossary.Web.Api.Modules.Glossary.Interfaces

{
    public interface IGlossaryService 
    {
        Task<IEnumerable<Term>> GetAll(CancellationToken cancellationToken);
        Task<Term?> GetById(int id);
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <returns>The id of the new term record</returns>
        Task<int> Insert(Term term);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The count of the number of records affected by change</returns>
        Task<int> DeleteById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <returns>The count of the number of records affected by change</returns>
        Task<int> Update(Term term);

    }
}
