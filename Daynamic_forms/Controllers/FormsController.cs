using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Daynamic_forms.Data;
using Daynamic_forms.Model;

using System.Net.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Localization;

namespace Daynamic_forms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FormsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Forms
        [HttpGet("GetForms")]
        public async Task<IActionResult> Getforms()
            {
            var form = await _context.forms
                .Include(f => f.Question)
                .ToListAsync();
            //var question = await _context.questions.ToListAsync();

            //var Details = (
            //   from f in forms
            //   select new
            //   {
            //       form_Fid = f.FID,
            //       form_subject = f.subject,
            //       form_description = f.description,

            //       classes = (from q in forms.Ques
            //                  where f.FID == q.Forms.f
            //                  select new
            //                  {
            //                      form_id = q.Forms.FID,
            //                      form_questionns = q.questions,
            //                      type = q.anstype
            //                  })
            //   }).ToList();

            return Ok(form);
        }

        // GET: api/Forms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Forms>>> GetForms(int id)
        {
            var form = await _context.forms
                .Where(f => f.FID == id)
                .Include(f => f.Question)
                .ToListAsync();
            return form;
        }

        // PUT: api/Forms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpGet("GetEdit")]
        public async Task<ActionResult<List<Forms>>> GetEdit(int id)
        {
            var form = await _context.forms
                .Where(f => f.FID == id)
                .Include(f => f.Question)
                .ToListAsync();

            return form;
        }


        [HttpPost("PostForms")]
        public async Task<IActionResult> PostForms(Forms forms)
        {
            if (ModelState.IsValid)
            {
                _context.forms.Add(forms);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        //public IActionResult PostForms(Forms forms)
        //{
        //    //var FID = Guid.NewGuid();
        //    //Forms model = new Forms();
        //    //model.FID = FID;
        //    //model.subject = forms.description;
        //    //model.description = forms.description;
        //    //model.quesions = forms.quesions;
        //    //_context.forms.Add(forms);
        //    //foreach (var value in forms.quesions)
        //    //{
        //    //        Question O = new Question();
        //    //        O.questions = value.questions;
        //    //        O.anstype = value.anstype;
        //    //        O.FID = FID;
        //    //        _context.questions.Add(O);
        //    //}
        //    if (ModelState.IsValid)
        //    {
        //        _context.forms.Add(forms);
        //        _context.SaveChangesAsync();
        //    }
        //    return Ok();
        //}
        // DELETE: api/Forms/5

       
        [HttpPut("PutForms")]
        public async Task<IActionResult> PutForms( Forms forms)
        {
            List<Question> authors = new List<Question>();
            foreach (var q in forms.Question)
            {
                authors.Add(q);
            }

            _context.Entry(forms).State = EntityState.Modified;
            var quest = _context.question.Where(f => f.FormsId == forms.FID);
            foreach (var c in quest)
            {
                _context.question.Remove(c);
            }
            try
            {
               
                foreach (var value in authors)
                {
                    _context.question.Add(value);
                }
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!FormsExists(forms.FID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

          
           
            return Ok();
        }



        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteForms(int id)
        {
            var forms = await _context.forms.FindAsync(id);
            if (forms == null)
            {
                return NotFound();
            }

            _context.forms.Remove(forms);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FormsExists(int id)
        {
            return _context.forms.Any(e => e.FID == id);
        }
    }
}








//var quest = _context.question.Where(f => f.FormsId == forms.FID);
//if (quest == null)
//{
//    return NotFound();
//}
//foreach (var c in quest)
//{
//    _context.question.Remove(c);
//}


//Question O = new Question();
//foreach (var value in forms.Question)
//{

//    O.questions = value.questions;
//    O.anstype = value.anstype;
//    O.answer = value.answer;
//    O.FormsId = forms.FID;

//}

//foreach (var q in forms.Question)
//{
//    _context.Entry(q).State = EntityState.Modified;
//    if (q.QID == 0)
//    {
//        Question O = new Question();
//        O.questions = q.questions;
//        O.anstype = q.anstype;
//        O.answer = q.answer;
//        O.FormsId = forms.FID;
//        _context.question.Add(O);
//    }
//}
