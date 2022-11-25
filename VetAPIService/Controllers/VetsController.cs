using VetService.Domain.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetService.Domain.Repositories;
using VetService.Domain.Entities;
using VetService.Domain.ApiModels;

namespace VetAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VetsController : ControllerBase
    {
        private readonly IManagerDoctor _managerDoctor;
        private readonly IManagerPrescription _managerPrescription;
        private readonly IManagerSymptom _managerSymptom;
        private readonly IManagerTest _managerTest;
        private readonly IManagerVital _managerVital;

        private readonly ILogger<VetsController> _logger;

        public VetsController(IManagerDoctor managerDoctor, IManagerPrescription managerPrescription, IManagerSymptom managerSymptom, IManagerTest managerTest, IManagerVital managerVital, ILogger<VetsController> logger)
        {
            _managerDoctor = managerDoctor;
            _managerPrescription = managerPrescription;
            _managerSymptom = managerSymptom;
            _managerTest = managerTest;
            _managerVital = managerVital;
            _logger = logger;
        }

        [HttpGet("Doctor")]
        // [Produces(typesof(List<DoctorApiModels>))]
        public async Task<ActionResult<List<DoctorApiModel>>> GetAsync()
        {
            try
            {
                return new ObjectResult(await _managerDoctor.GetAllDoctorAsync());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, ex);
            }
        }
        [HttpGet("Doctor/{id}")]
        [Produces(typeof(List<DoctorApiModel>))]
        public async Task<ActionResult<DoctorApiModel>> GetAsync(int id)
        {
            try
            {
                var doctor = await _managerDoctor.GetDoctorByIdAsync(id);
                if (doctor == null)
                {
                    return NotFound();
                }

                return Ok(doctor);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpPost("Doctor")]
        public async Task<ActionResult<DoctorApiModel>> PostAsync([FromBody] DoctorApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, await _managerDoctor.AddDoctorAsync(input));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpPut("Doctor/{id}")]
        public async Task<ActionResult<DoctorApiModel>> PutAsync(int id, [FromBody] DoctorApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (await _managerDoctor.GetDoctorByIdAsync(id) == null)
                {
                    return NotFound();
                }
                if (await _managerDoctor.UpdateDoctorAsync(input))
                {
                    return Ok(input);
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpDelete("Doctor/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                if (await _managerDoctor.GetDoctorByIdAsync(id) == null)
                {
                    return NotFound();
                }

                if (await _managerDoctor.DeleteDoctorAsync(id))
                {
                    return Ok();
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        //Prescription Controller
        //--------------------------------------------
        //--------------------------------------------
        [HttpGet("Prescription")]
        public async Task<ActionResult<List<PrescriptionApiModel>>> GetPrecriptionAsync()
        {
            try
            {
                return new ObjectResult(await _managerPrescription.GetAllPrescriptionAsync());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, ex);
            }
        }
        [HttpGet("Prescription/{id}")]
        [Produces(typeof(List<PrescriptionApiModel>))]
        public async Task<ActionResult<PrescriptionApiModel>> GetPrescriptionAsync(int id)
        {
            try
            {
                var prescription = await _managerPrescription.GetPrescriptionByIdAsync(id);
                if (prescription == null)
                {
                    return NotFound();
                }

                return Ok(prescription);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpPost("Prescription")]
        public async Task<ActionResult<PrescriptionApiModel>> PostAsync([FromBody] PrescriptionApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, await _managerPrescription.AddPrescriptionAsync(input));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpPut("Prescription/{id}")]
        public async Task<ActionResult<PrescriptionApiModel>> PutAsync(int id, [FromBody] PrescriptionApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (await _managerPrescription.GetPrescriptionByIdAsync(id) == null)
                {
                    return NotFound();
                }
                if (await _managerPrescription.UpdatePrescriptionAsync(input))
                {
                    return Ok(input);
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpDelete("Prescription/{id}")]
        public async Task<ActionResult> DeletePrescriptionAsync(int id)
        {
            try
            {
                if (await _managerPrescription.GetPrescriptionByIdAsync(id) == null)
                {
                    return NotFound();
                }

                if (await _managerPrescription.DeletePrescriptionAsync(id))
                {
                    return Ok();
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        //SymptomController
        //---------------------
        //--------------------------
        [HttpGet("Symptom")]
        public async Task<ActionResult<List<SymptomApiModel>>> GetSymptomAsync()
        {
            try
            {
                return new ObjectResult(await _managerSymptom.GetAllSymptomAsync());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, ex);
            }
        }
        [HttpGet("Symptom/{id}")]
        [Produces(typeof(List<SymptomApiModel>))]
        public async Task<ActionResult<SymptomApiModel>> GetSymptomAsync(int id)
        {
            try
            {
                var symptom = await _managerSymptom.GetSymptomByIdAsync(id);
                if (symptom == null)
                {
                    return NotFound();
                }

                return Ok(symptom);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpPost("Symptom")]
        public async Task<ActionResult<SymptomApiModel>> PostAsync([FromBody] SymptomApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, await _managerSymptom.AddSymptomAsync(input));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpPut("Symptom/{id}")]
        public async Task<ActionResult<SymptomApiModel>> PutAsync(int id, [FromBody] SymptomApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (await _managerSymptom.GetSymptomByIdAsync(id) == null)
                {
                    return NotFound();
                }
                if (await _managerSymptom.UpdateSymptomAsync(input))
                {
                    return Ok(input);
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpDelete("Symptom/{id}")]
        public async Task<ActionResult> DeleteSymptomAsync(int id)
        {
            try
            {
                if (await _managerSymptom.GetSymptomByIdAsync(id) == null)
                {
                    return NotFound();
                }

                if (await _managerSymptom.DeleteSymptomAsync(id))
                {
                    return Ok();
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        //TestController
        //----------------------------------------
        //----------------------------------------
        [HttpGet("Test")]
        public async Task<ActionResult<List<TestApiModel>>> GetTestAsync()
        {
            try
            {
                return new ObjectResult(await _managerTest.GetAllTestAsync());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, ex);
            }
        }
        [HttpGet("Test/{id}")]
        [Produces(typeof(List<TestApiModel>))]
        public async Task<ActionResult<TestApiModel>> GetTestAsync(int id)
        {
            try
            {
                var test = await _managerTest.GetTestByIdAsync(id);
                if (test == null)
                {
                    return NotFound();
                }

                return Ok(test);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpPost("Test")]
        public async Task<ActionResult<TestApiModel>> PostAsync([FromBody] TestApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, await _managerTest.AddTestAsync(input));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpPut("Test/{id}")]
        public async Task<ActionResult<TestApiModel>> PutAsync(int id, [FromBody] TestApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (await _managerTest.GetTestByIdAsync(id) == null)
                {
                    return NotFound();
                }
                if (await _managerTest.UpdateTestAsync(input))
                {
                    return Ok(input);
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpDelete("Test/{id}")]
        public async Task<ActionResult> DeleteTestAsync(int id)
        {
            try
            {
                if (await _managerTest.GetTestByIdAsync(id) == null)
                {
                    return NotFound();
                }

                if (await _managerTest.DeleteTestAsync(id))
                {
                    return Ok();
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }
        //VitalController
        //-------------------------
        //-------------------------
        [HttpGet("Vital")]
        public async Task<ActionResult<List<VitalApiModel>>> GetVitalAsync()
        {
            try
            {
                return new ObjectResult(await _managerVital.GetAllVitalAsync());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(500, ex);
            }
        }
        [HttpGet("Vital/{id}")]
        [Produces(typeof(List<VitalApiModel>))]
        public async Task<ActionResult<VitalApiModel>> GetVitalAsync(int id)
        {
            try
            {
                var vital = await _managerVital.GetVitalByIdAsync(id);
                if (vital == null)
                {
                    return NotFound();
                }

                return Ok(vital);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpPost("Vital")]
        public async Task<ActionResult<VitalApiModel>> PostAsync([FromBody] VitalApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, await _managerVital.AddVitalAsync(input));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpPut("Vital/{id}")]
        public async Task<ActionResult<VitalApiModel>> PutAsync(int id, [FromBody] VitalApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (await _managerVital.GetVitalByIdAsync(id) == null)
                {
                    return NotFound();
                }
                if (await _managerVital.UpdateVitalAsync(input))
                {
                    return Ok(input);
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }

        [HttpDelete("Vital/{id}")]
        public async Task<ActionResult> DeleteVitalAsync(int id)
        {
            try
            {
                if (await _managerVital.GetVitalByIdAsync(id) == null)
                {
                    return NotFound();
                }

                if (await _managerVital.DeleteVitalAsync(id))
                {
                    return Ok();
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return StatusCode(500, ex);
            }
        }
    }
}




