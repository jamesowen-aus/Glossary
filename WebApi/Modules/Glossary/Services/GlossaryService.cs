using Glossary.Web.Api.Modules.Glossary.Model;
using Glossary.Web.Api.Modules.Glossary.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Glossary.Web.Api.Modules.Glossary.Services
{
    public class GlossaryService : IGlossaryService
    {
        private readonly GlossaryContext _dbContext;

        public GlossaryService(GlossaryContext glossaryContext) {
            _dbContext = glossaryContext;
        }

        public async Task<IEnumerable<Term>> GetAll(CancellationToken cancellationToken)
        {
            var items = await _dbContext.Terms
                .Include(term => term.Definition)
                .OrderBy(i => i.Name)
                .ToListAsync(cancellationToken);

            return items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A count of the number of records affected and 0 of unsuccessful</returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> DeleteById(int id)
        {
            try
            {
                var existingItem = await GetById(id);
                if (existingItem != null)
                {
                    _dbContext.Terms.Remove(existingItem);
                    return await _dbContext.SaveChangesAsync();
                }
                return 0;
            }
            catch (DbException exc)
            {
                throw new Exception(exc.Message);
            }
        }

        public async Task<Term?> GetById(int id)
        {
            var item = await _dbContext.Terms
                .Include(term => term.Definition)
                .FirstOrDefaultAsync(term => term.Id == id);
            return item;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="term">term.termId value is ignored for inserts</param>
        /// <returns>The new Id of the newly created Term record </returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> Insert(Term term)
        {
            try
            {
                term.Id = 0;
                await _dbContext.Terms.AddAsync(term);
                await _dbContext.SaveChangesAsync();
                return term.Id;
            }
            catch (DbException exc)
            {
                throw new Exception(exc.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="term">term.termId property used as PK key to identify term for updating</param>
        /// <returns>The number of affected records</returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> Update(Term term)
        {
            try
            {
                var existingItem = await GetById(term.Id);
                if (existingItem != null)
                {
                    _dbContext.Terms.Update(existingItem);
                    return await _dbContext.SaveChangesAsync();
                }
                return 0;

            } catch (DbException exc)
            {   
                throw new Exception(exc.Message);
            }
        }
        
    }
}
