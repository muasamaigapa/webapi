using Model;
using Model.Models;
using System.Linq;
using System.Web.Http;
using WebApplication4.Infrastructure;
using WebApplication4.ViewModel;

namespace WebApplication4.Controllers
{
    public class StudentController : ApiController
    {
        private ApiDbContext _db;

        public StudentController()
        {
            this._db = new ApiDbContext();
        }

        /**
		 * @api {Post} /Student/Create Tạo mới 1 sinh viên
		 * @apigroup Student
		 * @apiPermission none
		 * @apiVersion 1.0.0
		 * 
		 * @apiParam {string} Code Mã của sinh viên mới
		 * @apiParam {string} Name Tên của sinh viên mới
         * @apiParam {string} BirthDay Ngày sinh của sinh viên mới
         * @apiParam {string} Address Địa chỉ của sinh viên mới
         * @apiParam {int} Class_Id Id Lớp của sinh viên mới
		 * 
		 * @apiParamExample {json} Request-Example: 
		 * {
		 *		Code: "SV01",
		 *		Name: "Hồ Tấn Mỹ",
         *		BirthDay: "01/01/2018",
         *		Address: "Quận Bình Thạnh, Hồ Chí Minh",
         *		Class_Id: 1
		 * }
		 * 
         * @apiSuccess {string} Code Mã của sinh viên vừa tạo
		 * @apiSuccess {string} Name Tên của sinh viên vừa tạo
         * @apiSuccess {string} BirthDay Ngày sinh của sinh viên vừa tạo
         * @apiSuccess {string} Address Địa chỉ của sinh viên vừa tạo
         * @apiSuccess {int} Class_Id Id Lớp của sinh viên vừa tạo
		 * 
		 * @apiSuccessExample {json} Response: 
		 * {
		 *		Id: 1,
		 *		Code: "SV01",
		 *		Name: "Hồ Tấn Mỹ",
         *		BirthDay: "01/01/2018",
         *		Address: "Quận Bình Thạnh, Hồ Chí Minh",
         *		Class_Id: 1
		 * }
		 * 
		 * @apiError (Error 400) {string[]} Errors Mảng các lỗi
		 * 
		 * @apiErrorExample: {json}
		 * {
		 *		"Errors" : [
		 *			"Mã sinh viên là trường bắt buộc",
		 *			"Tên sinh viên là trường bắt buộc"
		 *		]
		 * }
         * @apiErrorExample: {json}
		 * {
         *		"Errors" : [
		 *			"Không tìm thấy mã lớp"
		 *		]
		 * }
		 * 
		 */
        [HttpPost]
        public IHttpActionResult Create(CreateStudentMdoel model)
        {
            IHttpActionResult httpActionResult;
            ErrorsModel errors = new ErrorsModel();

            if (string.IsNullOrEmpty(model.Code))
            {
                errors.Add("Mã sinh viên là trường bắt buộc");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                errors.Add("Tên sinh viên là trường bắt buộc");
            }

            if (_db.Classes.FirstOrDefault(m => m.Id == model.Class_Id) == null)
            {
                errors.Add("Không tìm thấy lớp này");
                
            }

            if (errors.Errors.Count == 0)
            {
                Students sv = new Students();
                sv.Code = model.Code;
                sv.Name = model.Name;
                sv.BirthDay = model.BirthDay;
                sv.Address = model.Address;
                sv = _db.Students.Add(sv);

                this._db.SaveChanges();

                StudentModel viewModel = new StudentModel(sv);

                httpActionResult = Ok();
            }
            else
            {
                httpActionResult = new ErrorActionResult(Request, System.Net.HttpStatusCode.BadRequest, errors);
            }
            return httpActionResult;
        }

        /**
		 * @api {Post} /Student/Update Cập nhật 1 sinh viên
		 * @apigroup Student
		 * @apiPermission none
		 * @apiVersion 1.0.0
		 *
         * @apiParam {int} Id Id của sinh viên cần cập nhật
         * @apiParam {string} Code Mã của sinh viên cần cập nhật
		 * @apiParam {string} Name Tên của sinh viên cần cập nhật
         * @apiParam {string} BirthDay Ngày sinh của sinh viên cần cập nhật
         * @apiParam {string} Address Địa chỉ của sinh viên cần cập nhật
         * @apiParam {int} Class_Id Id Lớp của sinh viên cần cập nhật
		 * 
		 * @apiParamExample {json} Request-Example: 
		 * {
         *      Id: 1,
		 *		Code: "SV02",
		 *		Name: "CLAY",
         *		BirthDay: "02/01/2018",
         *		Address: "Phường 25, Quận Bình Thạnh, Hồ Chí Minh",
         *		Class_Id: 2
		 * }
		 * 
         * @apiSuccess {int} Id Id của sinh viên vừa cập nhật
         * @apiSuccess {string} Code Mã của sinh viên vừa cập nhật
		 * @apiSuccess {string} Name Tên của sinh viên vừa cập nhật
         * @apiSuccess {string} BirthDay Ngày sinh của sinh viên vừa cập nhật
         * @apiSuccess {string} Address Địa chỉ của sinh viên vừa cập nhật
         * @apiSuccess {int} Class_Id Id Lớp của sinh viên vừa cập nhật
		 * 
		 * @apiSuccessExample {json} Response: 
		 * {
		 *		Id: 1,
		 *		Code: "SV02",
		 *		Name: "CLAY",
         *		BirthDay: "02/01/2018",
         *		Address: "Phường 25, Quận Bình Thạnh, Hồ Chí Minh",
         *		Class_Id: 2
		 * }
		 * 
		 * @apiError (Error 400) {string[]} Errors Mảng các lỗi
		 * 
		 * @apiErrorExample: {json}
		 * {
		 *		"Errors" : [
		 *			"Mã sinh viên là trường bắt buộc",
		 *			"Tên sinh viên là trường bắt buộc"
		 *		]
		 * }
         * @apiErrorExample: {json}
         * {
         *      "Errors" : [
		 *			"Không tìm thấy mã lớp"
		 *		]
         * }
		 * 
		 */
        [HttpPut]
        public IHttpActionResult Update(UpdateStudentModel model)
        {
            IHttpActionResult httpActionResult;
            ErrorsModel errors = new ErrorsModel();

            Students sv = this._db.Students.FirstOrDefault(x => x.Id == model.Id);

            if (sv == null)
            {
                errors.Add("Không tìm thấy mã sinh viên này");

                httpActionResult = new ErrorActionResult(Request, System.Net.HttpStatusCode.NotFound, errors);
            }
            else
            {
                sv.Code = model.Code ?? model.Code;
                sv.Name = model.Name ?? model.Name;
                sv.BirthDay = model.BirthDay ?? model.BirthDay;
                sv.Address = model.Address ?? model.Address;

                this._db.Entry(sv).State = System.Data.Entity.EntityState.Modified;

                this._db.SaveChanges();

                httpActionResult = Ok(new StudentModel(sv));
            }

            return httpActionResult;
        }


        /**
		 * @api {Post} /Student/GetAll Lấy tất cả sinh viên
		 * @apigroup Student
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
		 *		Code: "SV02",
		 *		Name: "CLAY",
         *		BirthDay: "02/01/2018",
         *		Address: "Phường 25, Quận Bình Thạnh, Hồ Chí Minh",
         *		Class_Id: 2
		 * }
         * @apiSuccessExample {json} Response: 
		 * {
		 *		Id: 2,
		 *		Code: "SV01",
		 *		Name: "Hồ Tấn Mỹ",
         *		BirthDay: "02/01/2018",
         *		Address: "Phường 25, Quận Bình Thạnh, Hồ Chí Minh",
         *		Class_Id: 2
		 * }
		 * 
		 */
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var dssv = this._db.Students.Select(x => new StudentModel()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                BirthDay = x.BirthDay,
                Address = x.Address,
                Class_Id = x.Class.Id
            });

            return Ok(dssv);
        }


        /**
		 * @api {Post} /Student/GetById/1 Lấy sinh viên có Id là 1
		 * @apigroup Student
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
		 *		Code: "SV02",
		 *		Name: "CLAY",
         *		BirthDay: "02/01/2018",
         *		Address: "Phường 25, Quận Bình Thạnh, Hồ Chí Minh",
         *		Class_Id: 2
		 * }
		 * 
		 * @apiError (Error 400) {string[]} Errors Mảng các lỗi
		 * 
		 * @apiErrorExample: {json}
		 * {
         *		"Errors" : [
		 *			"Không tìm thấy sinh viên này"
		 *		]
		 * }
		 * 
		 */
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            IHttpActionResult httpActionResult;
            var sv = _db.Students.FirstOrDefault(x => x.Id == id);

            if (sv == null)
            {
                ErrorsModel errors = new ErrorsModel();
                errors.Add("Không tìm thấy mã sinh viên này");

                httpActionResult = new ErrorActionResult(Request, System.Net.HttpStatusCode.NotFound, errors);
            }
            else
            {
                httpActionResult = Ok(new StudentModel(sv));
            }

            return httpActionResult;
        }
    }
}