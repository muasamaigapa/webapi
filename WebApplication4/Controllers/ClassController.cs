using Model;
using Model.Models;
using System.Linq;
using System.Web.Http;
using WebApplication4.Infrastructure;
using WebApplication4.ViewModel;

namespace WebApplication4.Controllers
{
    public class ClassController : ApiController
    {
        private ApiDbContext _db;

        public ClassController()
        {
            this._db = new ApiDbContext();
        }

        /**
		 * @api {Post} /Class/Create Tạo mới 1 lớp
		 * @apigroup Class
		 * @apiPermission none
		 * @apiVersion 1.0.0
		 * 
		 * @apiParam {string} Code Mã của lớp mới
		 * @apiParam {string} Name Tên của lớp mới
         * @apiParam {int} HomeroomTeacher_Id Id của giảng viên chủ nhiệm
         * @apiParam {int} Teacher_Id Id của giảng viên
		 * 
		 * @apiParamExample {json} Request-Example: 
		 * {
		 *		Code: "GV01",
		 *		Name: "Nguyễn Văn A",
         *		HomeroomTeacher_Id: 1,
         *		Teacher_Id: 1
		 * }
		 * 
         * @apiSuccess {string} Code Mã của lớp vừa tạo
		 * @apiSuccess {string} Name Tên của lớp vừa tạo
         * @apiSuccess {int} BirthDay Ngày sinh của lớp vừa tạo
         * @apiSuccess {int} Address Địa chỉ của lớp vừa tạo
		 * 
		 * @apiSuccessExample {json} Response: 
		 * {
		 *		Id: 1,
		 *		Code: "GV01",
		 *		Name: "Nguyễn Văn A",
         *		HomeroomTeacher_Id: 1,
         *		Teacher_Id: 1
		 * }
		 * 
		 * @apiError (Error 400) {string[]} Errors Mảng các lỗi
		 * 
		 * @apiErrorExample: {json}
		 * {
		 *		"Errors" : [
		 *			"Mã lớp là trường bắt buộc",
		 *			"Tên lớp là trường bắt buộc"
		 *		]
		 * }
         * @apiErrorExample: {json}
		 * {
         *		"Errors" : [
		 *			"Id giảng viên chu nhiệm không tồn tại"
		 *		]
		 * }
		 * 
		 */
        [HttpPost]
        public IHttpActionResult Create(CreateClass model)
        {
            IHttpActionResult httpActionResult;
            ErrorsModel errors = new ErrorsModel();

            if (string.IsNullOrEmpty(model.Code))
            {
                errors.Add("Mã lớp là trường bắt buộc");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                errors.Add("Tên lớp là trường bắt buộc");
            }

            if (_db.Teachers.FirstOrDefault(m => m.Id == model.HomeroomTeacher_Id) == null)
            {
                errors.Add("Id giảng viên chu nhiệm không tồn tại");
            }

            if (errors.Errors.Count == 0)
            {
                Classes l = new Classes();
                l.Code = model.Code;
                l.Name = model.Name;
                l.HomeroomTeacher = _db.Teachers.FirstOrDefault(x => x.Id == model.HomeroomTeacher_Id);
                l.Teacher = _db.Teachers.FirstOrDefault(x => x.Id == model.Teacher_Id);
                l = _db.Classes.Add(l);

                this._db.SaveChanges();

                ClassModel viewModel = new ClassModel(l);

                httpActionResult = Ok(viewModel);
            }
            else
            {
                httpActionResult = new ErrorActionResult(Request, System.Net.HttpStatusCode.BadRequest, errors);
            }

            return httpActionResult;
        }

        /**
		 * @api {Post} /Class/Update Cập nhật 1 lớp
		 * @apigroup Class
		 * @apiPermission none
		 * @apiVersion 1.0.0
		 * 
         * @apiParam {int}  Id Id lớp cần cập nhật
         * @apiParam {string} Code Mã của lớp cần cập nhật
		 * @apiParam {string} Name Tên của lớp cần cập nhật
         * @apiParam {int} HomeroomTeacher_Id Id của giảng viên chủ nhiệm cần cập nhật
         * @apiParam {int} Teacher_Id Id của giảng viên cần cập nhật
		 * 
		 * @apiParamExample {json} Request-Example: 
		 * {
         *      Id: 1,
		 *		Code: "GV02",
		 *		Name: "Nguyễn Văn B",
         *		HomeroomTeacher_Id: 1,
         *		Teacher_Id: 1
		 * }
		 * 
         * 
         * @apiParam {int}  Id Id lớp vừa cập nhật
         * @apiParam {string} Code Mã của lớp vừa cập nhật
		 * @apiParam {string} Name Tên của lớp vừa cập nhật
         * @apiParam {int} HomeroomTeacher_Id Id của giảng viên chủ nhiệm vừa cập nhật
         * @apiParam {int} Teacher_Id Id của giảng viên vừa cập nhật
		 * 
		 * @apiSuccessExample {json} Response: 
		 * {
		 *		Id: 1,
		 *		Code: "GV02",
		 *		Name: "Nguyễn Văn B",
         *		HomeroomTeacher_Id: 1,
         *		Teacher_Id: 1
		 * }
		 * 
		 * @apiError (Error 400) {string[]} Errors Mảng các lỗi
		 * 
		 * @apiErrorExample: {json}
		 * {
		 *		"Errors" : [
		 *			"Mã lớp là trường bắt buộc",
		 *			"Tên lớp là trường bắt buộc"
		 *		]
		 * }
         * @apiErrorExample: {json}
         * {
         *      "Errors" : [
		 *			"Id lớp không tồn tại"
		 *		]
         * }
		 * 
		 */
        [HttpPut]
        public IHttpActionResult Update(UpdateClass model)
        {
            IHttpActionResult httpActionResult;
            ErrorsModel errors = new ErrorsModel();

            Classes l = this._db.Classes.FirstOrDefault(x => x.Id == model.Id);

            if (l == null)
            {
                errors.Add("Id lớp không tồn tại");

                httpActionResult = new ErrorActionResult(Request, System.Net.HttpStatusCode.NotFound, errors);
            }
            else
            {
                l.Code = model.Code ?? model.Code;
                l.Name = model.Name ?? model.Name;
                l.HomeroomTeacher = _db.Teachers.FirstOrDefault(x => x.Id == model.HomeroomTeacher_Id) ?? _db.Teachers.FirstOrDefault(x => x.Id == model.HomeroomTeacher_Id);
                l.Teacher = _db.Teachers.FirstOrDefault(x => x.Id == model.Teacher_Id) ?? _db.Teachers.FirstOrDefault(x => x.Id == model.Teacher_Id);

                this._db.Entry(l).State = System.Data.Entity.EntityState.Modified;

                this._db.SaveChanges();

                httpActionResult = Ok(new ClassModel(l));
            }

            return httpActionResult;
        }


        /**
		 * @api {Post} /Class/GetAll Lấy tất cả lớp
		 * @apigroup Class
		 * @apiPermission none
		 * @apiVersion 1.0.0
		 * 
         * @apiSuccess {int} Id Id của lớp
         * @apiSuccess {string} Code Mã của lớp
		 * @apiSuccess {string} Name Tên của lớp
         * @apiSuccess {int} HomeroomTeacher_Id Id giảng viên chủ nhiệm
         * @apiSuccess {int} Teacher_Id Id của giảng viên
		 * 
		 * @apiSuccessExample {json} Response: 
		 * {
		 *		Id: 1,
		 *		Code: "GV02",
		 *		Name: "Nguyễn Văn B",
         *		HomeroomTeacher_Id: 1,
         *		Teacher_Id: 1
		 * }
         * @apiSuccessExample {json} Response: 
		 * {
		 *		Id: 2,
		 *		Code: "GV01",
		 *		Name: "Nguyễn Văn A",
         *		HomeroomTeacher_Id: 1,
         *		Teacher_Id: 1
		 * }
		 * 
		 */
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var dsl = this._db.Classes.Select(x => new ClassModel()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                HomeroomTeacher_Id = x.HomeroomTeacher.Id,
                Teacher_Id = x.Teacher.Id
            });

            return Ok(dsl);
        }

        /**
		 * @api {Post} /Class/GetById/1 Lấy lớp có Id là 1
		 * @apigroup Class
		 * @apiPermission none
		 * @apiVersion 1.0.0
		 * 
         * @apiSuccess {int} Id Id của sinh viên
         * @apiSuccess {string} Code Mã của sinh viên
		 * @apiSuccess {string} Name Tên của sinh viên
         * @apiSuccess {string} BirthDay Ngày sinh của sinh viên
         * @apiSuccess {string} Address Địa chỉ của sinh viên
         * @apiSuccess {int} Class_Id Id Lớp của sinh viên
		 * 
		 * @apiSuccessExample {json} Response: 
		 * {
		 *		Id: 1,
		 *		Code: "GV02",
		 *		Name: "Nguyễn Văn B",
         *		HomeroomTeacher_Id: 1,
         *		Teacher_Id: 1
		 * }
		 * 
		 * @apiError (Error 400) {string[]} Errors Mảng các lỗi
		 * 
		 * @apiErrorExample: {json}
		 * {
         *		"Errors" : [
		 *			"Không tìm thấy lớp này"
		 *		]
		 * }
         */
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            IHttpActionResult httpActionResult;
            var l = _db.Classes.FirstOrDefault(x => x.Id == id);

            if (l == null)
            {
                ErrorsModel errors = new ErrorsModel();
                errors.Add("Không tìm thấy lớp này");

                httpActionResult = new ErrorActionResult(Request, System.Net.HttpStatusCode.NotFound, errors);
            }
            else
            {
                httpActionResult = Ok(new ClassModel(l));
            }

            return httpActionResult;
        }
    }
}
 