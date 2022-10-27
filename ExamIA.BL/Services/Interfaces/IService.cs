using ExamIA.BL.DomainObjects;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ExamIA.BL.Services.Interfaces
{
    public interface IService
    {
        public Task<ActionResult<ResponseObject>> Get();
        public Task<ActionResult<ResponseObject>> GetById(int id);
        public Task<ActionResult<ResponseObject>> Create(object objectDto);
        public Task<ActionResult<ResponseObject>> Update(int id, object objectDto);
        public Task<ActionResult<ResponseObject>> PartialUpdate(int id, JsonPatchDocument<object> patchDocument);
        public Task<ActionResult<ResponseObject>> Delete(int id);
    }
}
