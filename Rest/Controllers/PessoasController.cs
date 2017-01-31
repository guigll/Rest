using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Rest.Models;

namespace Rest.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Rest.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Pessoa>("Pessoas");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PessoasController : ODataController
    {
        private RestContext db = new RestContext();

        // GET: odata/Pessoas
        [EnableQuery]
        public IQueryable<Pessoa> GetPessoas()
        {
            return db.Pessoas;
        }

        // GET: odata/Pessoas(5)
        [EnableQuery]
        public SingleResult<Pessoa> GetPessoa([FromODataUri] int key)
        {
            return SingleResult.Create(db.Pessoas.Where(pessoa => pessoa.ID == key));
        }

        // PUT: odata/Pessoas(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Pessoa> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Pessoa pessoa = await db.Pessoas.FindAsync(key);
            if (pessoa == null)
            {
                return NotFound();
            }

            patch.Put(pessoa);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(pessoa);
        }

        // POST: odata/Pessoas
        public async Task<IHttpActionResult> Post(Pessoa pessoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pessoas.Add(pessoa);
            await db.SaveChangesAsync();

            return Created(pessoa);
        }

        // PATCH: odata/Pessoas(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Pessoa> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Pessoa pessoa = await db.Pessoas.FindAsync(key);
            if (pessoa == null)
            {
                return NotFound();
            }

            patch.Patch(pessoa);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(pessoa);
        }

        // DELETE: odata/Pessoas(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Pessoa pessoa = await db.Pessoas.FindAsync(key);
            if (pessoa == null)
            {
                return NotFound();
            }

            db.Pessoas.Remove(pessoa);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PessoaExists(int key)
        {
            return db.Pessoas.Count(e => e.ID == key) > 0;
        }
    }
}
