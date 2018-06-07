using Model;
using Model.Models;
using System.Linq;
using System.Web.Http;
using WebApplication4.Infrastructure;
using WebApplication4.ViewModel;

namespace WebApplication4.Controllers
{
    public class TeacherController : ApiController
    {
        private ApiDbContext _db;

        public TeacherController()
        {
            this._db = new ApiDbContext();
        }

        /**
		 * @api {Post} /Teacher/Create Thêm mới 1 giảng viên
		 * @apigroup Teacher
		 * @apiPermission none
		 * @apiVersion 1.0.0
		 * 
		 * @apiParam {string} Code Mã của giảng viên mới
		 * @apiParam {string} Name Tên của giảng viên mới
		 * 
		 * @apiParamExample {json} Request-Example: 
		 * {
		 *		Code: "GV01",
		 *		Name: "Nguyễn Văn A"
		 * }
		 * 
         * @apiSuccess {string} Code Mã của giảng viên vừa tạo
		 * @apiSuccess {string} Name Tên của giảng viên vừa tạo
		 * 
		 * @apiSuccessExample {json} Response: 
		 * {
		 *		Id: 1,
	     *      Code: "GV01",
		 *		Name: "Nguyễn Văn A"
		 * }
		 * 
		 * @apiError (Error 400) {string[]} Errors Mảng các lỗi
		 * 
		 * @apiErrorExample: {json}
		 * {
		 *		"Errors" : [
		 *			"Mã giảng viên là trường bắt buộc",
		 *			"Tên giảng viên là trường bắt buộc"
		 *		]
		 * }
		 * 
		 */

        [HttpPost]
        public IHttpActionResult Create(CreateTeacher model)
        {
            IHttpActionResult httpActionResult;
            ErrorsModel errors = new ErrorsModel();

            if (string.IsNullOrEmpty(model.Code))
            {
                errors.Add("Mã giảng viên là trường bắt buộc");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                errors.Add("Tên giảng viên là trường bắt buộc");
            }

            if (errors.Errors.Count == 0)
            {
                Teachers t = new Teachers();
                t.Code = model.Code;
                t.Name = model.Name;
                t = _db.Teachers.Add(t);

                this._db.SaveChanges();

                TeacherModel viewModel = new TeacherModel(t);

                httpActionResult = Ok(viewModel);
            }
            else
            {
                httpActionResult = new ErrorActionResult(Request, System.Net.HttpStatusCode.BadRequest, errors);
            }

            return httpActionResult;
        }

        /**
		 * @api {Post} /Teacher/Update Cập nhật 1 giảng viên
		 * @apigroup Teacher
		 * @apiPermission none
		 * @apiVersion 1.0.0
		 * 
         * @apiParam {int} Id Id của giảng viên cần cập nhật
         * @apiParam {string} Code Mã của giảng viên cần cập nhật
		 * @apiParam {string} Name Tên của giảng viên cần cập nhật
		 * 
		 * @apiParamExample {json} Request-Example: 
		 * {
         *      Id: 1,
	     *      Code: "GV02",
		 *		Name: "Nguyễn Văn B"
		 * }
		 * 
         * @apiSuccess {int} Id Id của giảng viên vừa cập nhật
         * @apiSuccess {string} Code Mã của giảng viên vừa cập nhật
		 * @apiSuccess {string} Name Tên của giảng viên vừa cập nhật
		 * 
		 * @apiSuccessExample {json} Response: 
		 * {
		 *		Id: 1,
	     *      Code: "GV02",
		 *		Name: "Nguyễn Văn B"
		 * }
		 * 
		 * @apiError (Error 400) {string[]} Errors Mảng các lỗi
		 * 
		 * @apiErrorExample: {json}
		 * {
		 *		"Errors" : [
		 *			"Mã giảng viên là trường bắt buộc",
		 *			"Tên giảng viên là trường bắt buộc"
		 *		]
		 * }
		 * 
		 */
        [HttpPut]
        public IHttpActionResult Update(UpdateTeacher model)
        {
            IHttpActionResult httpActionResult;
            ErrorsModel errors = new ErrorsModel();

            Teachers gv = this._db.Teachers.FirstOrDefault(x => x.Id == model.Id);

            if (gv == null)
            {
                errors.Add("Mã giảng viên không tồn tại hoặc rỗng");

                httpActionResult = new ErrorActionResult(Request, System.Net.HttpStatusCode.NotFound, errors);
            }
            else
            {
                gv.Code = model.Code ?? model.Code;
                gv.Name = model.Name ?? model.Name;

                this._db.Entry(gv).State = System.Data.Entity.EntityState.Modified;

                this._db.SaveChanges();

                httpActionResult = Ok(new TeacherModel(gv));
            }

            return httpActionResult;
        }

        /**
		 * @api {Post} /Teacher/GetAll Lấy tất cả giảng viên
		 * @apigroup Student
		 * @apiPermission none
		 * @apiVersion 1.0.0
		 * 
         * @apiSuccess {int} Id Id của giảng viên
         * @apiSuccess {string} Code Mã của giảng viên
		 * @apiSuccess {string} Name Tên của giảng viên
		 * 
		 * @apiSuccessExample {json} Response: 
		 * {
		 *		Id: 1,
	     *      Code: "GV02",
		 *		Name: "Nguyễn Văn B"
		 * }
         * @apiSuccessExample {json} Response: 
		 * {
		 *		Id: 2,
	     *      Code: "GV01",
		 *		Name: "Nguyễn Văn A"
		 * }
		 * 
		 */

        
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var dsgv = this._db.Teachers.Select(x => new TeacherModel()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name
            });

            return Ok(dsgv);
        }

        /**
		 * @api {Post} /Student/GetById/1 Lấy giảng viên có Id là 1
		 * @apigroup Student
		 * @apiPermission none
		 * @apiVersion 1.0.0
		 * 
         * @apiSuccess {int} Id Id của giảng viên
         * @apiSuccess {string} Code Mã của giảng viên
		 * @apiSuccess {string} Name Tên của giảng viên
		 * 
		 * @apiSuccessExample {json} Response: 
		 * {
		 *		Id: 1,
	     *      Code: "GV02",
		 *		Name: "Nguyễn Văn B"
		 * }
		 * 
		 * @apiError (Error 400) {string[]} Errors Mảng các lỗi
		 * 
		 * @apiErrorExample: {json}
		 * {
         *		"Errors" : [
		 *			"Không tìm thấy giảng viên này"
		 *		]
		 * }
		 * 
		 */

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            IHttpActionResult httpActionResult;
            var gv = _db.Teachers.FirstOrDefault(x => x.Id == id);

            if (gv == null)
            {
                ErrorsModel errors = new ErrorsModel();
                errors.Add("Không tìm thấy giảng viên này");

                httpActionResult = new ErrorActionResult(Request, System.Net.HttpStatusCode.NotFound, errors);
            }
            else
            {
                httpActionResult = Ok(new TeacherModel(gv));
            }

            return httpActionResult;
        }
    }
}