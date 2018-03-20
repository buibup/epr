using System;
using System.Collections.Generic;
using System.Web.Mvc;
using App.Data.Dao;
using App.Data.Model;
using System.Web;
using System.Net;
using System.Configuration;

namespace App.Controllers
{

    public class OpdRecordController : Controller
    {

        public ActionResult Index(FormCollection collection)
        {
            //STEP 1: get parameter from request.            
            String Hn = "";

            PatientDao patientDao = new PatientDao();
            int hnRowId = !String.IsNullOrEmpty(Request.Params["hnRowId"]) ? Int32.Parse(Request.Params["hnRowId"].ToString()) : 0;

            List<CareProvModel> listCare = patientDao.FindAllCareProve();

            if (hnRowId > 0)
            {
                patientDao = new PatientDao();
                PatientModel model = patientDao.FindByRowId(hnRowId);
                if (model != null)
                {
                    Hn = model.PAPMI_No;
                }
            }
            else
            {
                Hn = !String.IsNullOrEmpty(Request.Params["hn"]) ? Request.Params["hn"] : "";

            }

            ViewBag.Hn = Hn;
            ViewBag.Care = listCare;


            //Response.Cookies[".QuippeDemoSiteAuth"].Value = new QuippeDao().getCookieByLogin().Split('=')[1];
            //Response.Cookies[".QuippeDemoSiteAuth"].Expires = DateTime.Now.AddMinutes(1);

            return View();
        }



        //[HttpGet]
        //public ActionResult EDIT(CareProvModel objMd)
        //{
        //    PatientDao patientDao = new PatientDao();
        //    ViewBag.Care = new SelectList(patientDao.FindAllCareProve(), "CTPCP_RowId", "CTPCP_Desc", objMd);
        //    return View();
        //}

        [HttpPost]
        public ActionResult GetPatientInfo(String HNRowId)
        {
            try
            {
                SummaryDao SamDAO = new SummaryDao();
                PatientInfo PatientInfo = SamDAO.PatientInfobyRow(HNRowId);
                return Json(new
                {
                    success = true,
                    model = PatientInfo
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }




        [HttpPost]
        public ActionResult GetPatient(String Hn = "")
        {
            try
            {
                //STEP 1: get parameter from request.
                //Hn = !String.IsNullOrEmpty(collection.Get("hn")) ? collection["hn"].ToString() : "";
                //Hn = Request.Params["hn"];

                //STEP 2: find patient and all patient's episode.
                PatientDao patientDao = new PatientDao();
                PatientModel model = new PatientModel();
                DoctorChiefComPlainModel model2 = new DoctorChiefComPlainModel();
                string sPath = System.Web.HttpContext.Current.Request.Url.PathAndQuery;
                if ((Hn != "")) { model = patientDao.FindByHn(Hn); }

                //if (Hn != "") { model = patientDao.FindByHn(Hn, DoctorCode); } 

                if (model != null && model.PAPMI_RowId > 0)
                //if (Hn !="" || DoctorCode != "")
                {
                    //append episode listing.

                    List<EpisodeModel> list = patientDao.FindAllEpisode(model.PAPMI_RowId);
                    //List<DoctorChiefComPlainModel> DoctorReturn = new List<DoctorChiefComPlainModel>();
                    if (list == null)
                    {


                        list = new List<EpisodeModel>();
                    }


                    model.episodeList = list;


                    if (list.Count > 0)
                    {

                        foreach (EpisodeModel set in list)
                        {
                            String PAADMRowId = set.PAADMRowId;
                            //  set.DoctorNameModelList = patientDao.FindDoctorName(PAADMRowId);   
                            set.DoctorChiefComPainList = patientDao.FindChiefComplintList(PAADMRowId);

                        }
                    }



                    ////get QuippeList add to model
                    //List<QuippeModel> listQuippe = patientDao.FindAllQuippe(Hn);
                    //if (listQuippe == null)
                    //{
                    //    listQuippe = new List<QuippeModel>();
                    //}
                    //model.QuippeModelList = listQuippe;





                    //STEP 4: response result back to client.
                    return Json(new { success = true, Hn = Hn, model = model });
                }
                else
                {
                    throw new Exception("Patient reccord not found!");
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Hn = Hn, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetObservationLastEpi(String Hn = "")
        {
            try
            {

                PatientDao patientDao = new PatientDao();
                List<ObservationModel> model = patientDao.ObservationLastEpi(Hn);

                return Json(new { success = true, Hn = Hn, model = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Hn = Hn, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult GetMedical(String episodeRowId = "")
        {
            try
            {
                //STEP 1: get parameter from request.
                //episodeRowId = !String.IsNullOrEmpty(collection.Get("episodeRowId")) ? collection["episodeRowId"].ToString() : "";

                //STEP 2: get episode record.
                PatientDao patientDao = new PatientDao();
                EpisodeModel episode = patientDao.EpisodeRow(episodeRowId);
                if (episode == null)
                    throw new Exception("Requested episode is not found.");

                //STEP 3: load all summary information.
                SummaryDao summaryDao = new SummaryDao();
                vaccineDAO VaccineDAO = new vaccineDAO();
                episode.ClinicalNotesModelList = summaryDao.ClinicalNotes(episode.PAADMRowId);
                //episode.ClinicalNotesModelNewList = summaryDao.ClinicalNotesNew(episode.PAADMRowId);
                //episode.ConsultationOrderModelList = summaryDao.ConsultationOrder(episode.PAADMRowId);
                episode.AllergiesModelList = summaryDao.Allergies(episode.PAPMIRowId);
                episode.ObservationModelList = summaryDao.Observation(episode.PAADMRowId);
                episode.ObservationAllergyModelList = summaryDao.ObservationAllergy(episode.PAADMRowId);

                episode.PhyExamModelList = summaryDao.PhyExam(episode.PAADMRowId);
                episode.DiagnosisModelList = summaryDao.Diagnosis(episode.PAADMRowId);
                //episode.ProceduresModelList = summaryDao.Procedures(episode.PAADMRowId);
                episode.RadiologyResultsModelList = summaryDao.RadiologyResults(episode.PAADMRowId);
                episode.LaboratoryResultsModelList = summaryDao.LaboratoryResults(episode.PAPMINo, episode.EpisodeNo);
                episode.MedicalModelList = summaryDao.Medication(episode.EpisodeNo); //Medication

                //episode.MedicalModelDoctorList = summaryDao.MedicationDoctor(episode.EpisodeNo); //Find Doctor in Medication
                episode.MedicalModelDoctorList = summaryDao.MedicationDoctorCacheconn(episode.EpisodeNo); //Find Doctor in Medication
                //episode.CareProveList = patientDao.FindAllCareProve();
                episode.DoctorChiefComPainList = patientDao.FindChiefComplintList(episode.PAADMRowId); //ดึงหมอที่มี Chiefcomplant ตรงเมนูด้านซ้าย
                episode.DoctorChiefComPainAllList = patientDao.FindChiefComplintAllList(episode.PAADMRowId); //ดึงClinicalNotes  ทั้งหมด
                episode.DoctorChiefComPainDocList = patientDao.FindChiefComplintDocList(episode.PAADMRowId); ////ดึงหมอที่มี Chiefcomplant Detail
                episode.DoctorNameModelList = patientDao.FindDoctorName(episode.PAADMRowId); //ดึงหมอที่มี ที่มีนัดAppionment
                episode.DoctorChiefComPainNurseList = patientDao.FindChiefComplinNursetList(episode.PAADMRowId); //ดึงClinicalNotes CCN  
                episode.NursingInterventionList = summaryDao.GetNursingIntervention(episodeRowId);
                //episode.AppointmentList = VaccineDAO.getAppointmentList(episode.PAPMINo);


                episode.LocationList = new List<DoctorNameModel>();
                episode.NotDoctorLocationList = new List<DoctorNameModel>();
                episode.NotDoctorList = new List<DoctorNameModel>();
                episode.MedicalModelOtherList = new List<MedicationModel>();
                episode.DiagnosisModelOtherList = new List<DiagnosisModel>();
                episode.NotDoctorOtherList = new List<DoctorNameModel>();
                #region SortGroupByDoctorChiefComPain
                episode.DoctorNameModelListLast = new List<DoctorNameModel>();  // เรียงหมอตาม DoctorChiefComPain
                int countSameDoctor = 0;
                String DoctorCodeT = "";
                String DoctorCodeT1 = "";

                //---------Check Doctor------------------------------------------------
                if (episode.DoctorChiefComPainList.Count > 0)  // เรียงหมอตาม DoctorChiefComPain
                {
                    for (int j = 0; j < episode.DoctorChiefComPainList.Count; j++)
                    {
                        if (!String.IsNullOrEmpty(episode.DoctorChiefComPainList[j].DoctorCode))
                        {
                            if (j == 0)
                            {
                                DoctorCodeT = episode.DoctorChiefComPainList[j].DoctorCode;
                            }
                            else
                            {
                                DoctorCodeT = episode.DoctorChiefComPainList[j - 1].DoctorCode;
                                DoctorCodeT1 = episode.DoctorChiefComPainList[j].DoctorCode;
                            }

                            if (j == 0 || DoctorCodeT != DoctorCodeT1)
                            {
                                int counth = 0;
                                if (episode.DoctorNameModelListLast.Count > 0)
                                {
                                    for (int h = 0; h < episode.DoctorNameModelListLast.Count; h++)
                                    {
                                        if (episode.DoctorNameModelListLast[h].DoctorCode == episode.DoctorChiefComPainList[j].DoctorCode)
                                        {
                                            counth = counth + 1;

                                        }
                                    }


                                    if (counth == 0)
                                    {

                                        DoctorNameModel model = new DoctorNameModel();
                                        model.DoctorCode = episode.DoctorChiefComPainList[j].DoctorCode;
                                        model.DoctorDesc = episode.DoctorChiefComPainList[j].DoctorName;
                                        model.PAADMRowId = episode.PAADMRowId;
                                        model.LocationCode = episode.DoctorChiefComPainList[j].LocationCode;
                                        model.LocationDesc = episode.DoctorChiefComPainList[j].LocationDesc;
                                        model.CTPCPSMCNo = episode.DoctorChiefComPainList[j].CTPCPSMCNo;
                                        model.SubSpec = episode.DoctorChiefComPainList[j].SubSpec;
                                        model.DoctorDescENG = episode.DoctorChiefComPainList.Count > 0 ? episode.DoctorChiefComPainList[j].DoctorDescENG : "";
                                        model.DoctorRowNumber = j + 1;

                                        episode.DoctorNameModelListLast.Add(model);
                                        counth = 0;
                                    }


                                }
                                else
                                {
                                    DoctorNameModel model = new DoctorNameModel();
                                    model.DoctorCode = episode.DoctorChiefComPainList[j].DoctorCode;
                                    model.DoctorDesc = episode.DoctorChiefComPainList[j].DoctorName;
                                    model.PAADMRowId = episode.PAADMRowId;
                                    model.LocationCode = episode.DoctorChiefComPainList[j].LocationCode;
                                    model.LocationDesc = episode.DoctorChiefComPainList[j].LocationDesc;
                                    model.CTPCPSMCNo = episode.DoctorChiefComPainList[j].CTPCPSMCNo;
                                    model.SubSpec = episode.DoctorChiefComPainList[j].SubSpec;
                                    model.DoctorDescENG = episode.DoctorChiefComPainList.Count > 0 ? episode.DoctorChiefComPainList[j].DoctorDescENG : "";

                                    model.DoctorRowNumber = j + 1;
                                    episode.DoctorNameModelListLast.Add(model);
                                }

                            }
                        }
                    }










                    // Chiefcomplant ตรงเมนูด้านซ้าย มารวมกับ หมอที่มี ที่มีนัดAppionment-------------------
                    if (episode.DoctorNameModelList.Count > 0)
                    {
                        for (int i = 0; i < episode.DoctorNameModelList.Count; i++)
                        {
                            countSameDoctor = 0;
                            if (episode.DoctorNameModelListLast.Count > 0)
                            {

                                for (int k = 0; k < episode.DoctorNameModelListLast.Count; k++)
                                {

                                    if (episode.DoctorNameModelListLast[k].DoctorCode == episode.DoctorNameModelList[i].DoctorCode)
                                    {
                                        countSameDoctor = countSameDoctor + 1;
                                    }


                                }

                                if (countSameDoctor == 0)
                                {
                                    DoctorNameModel model = new DoctorNameModel();
                                    model.DoctorCode = episode.DoctorNameModelList[i].DoctorCode;
                                    model.DoctorDesc = episode.DoctorNameModelList[i].DoctorDesc;
                                    model.LocationCode = episode.DoctorNameModelList[i].LocationCode;
                                    model.LocationDesc = episode.DoctorNameModelList[i].LocationDesc;
                                    model.CTPCPSMCNo = episode.DoctorNameModelList[i].CTPCPSMCNo;
                                    model.SubSpec = episode.DoctorNameModelList[i].SubSpec;
                                    model.PAADMRowId = episode.PAADMRowId;
                                    model.DoctorRowNumber = episode.DoctorNameModelListLast.Count + 1;
                                    model.DoctorDescENG = episode.DoctorNameModelList.Count > 0 ? episode.DoctorNameModelList[i].DoctorDescENG : "";
                                    episode.DoctorNameModelListLast.Add(model);

                                }



                            }
                            else
                            {


                                int CounrSameDr2 = 0;

                                if (episode.DoctorNameModelList.Count > 0)
                                {
                                    for (int k = 0; k < episode.DoctorNameModelList.Count; k++)
                                    {

                                        if (episode.DoctorNameModelListLast.Count > 0)
                                        {

                                            for (int g = 0; g < episode.DoctorNameModelListLast.Count; g++)
                                            {

                                                if (episode.DoctorNameModelListLast[g].DoctorCode.Trim() == episode.DoctorNameModelList[k].DoctorCode.Trim())
                                                {
                                                    CounrSameDr2 = CounrSameDr2 + 1;
                                                }


                                            }

                                            if (CounrSameDr2 == 0)
                                            {
                                                DoctorNameModel model = new DoctorNameModel();
                                                model.DoctorCode = episode.DoctorNameModelList[k].DoctorCode;
                                                model.DoctorDesc = episode.DoctorNameModelList[k].DoctorDesc;
                                                model.LocationCode = episode.DoctorNameModelList[k].LocationCode;
                                                model.LocationDesc = episode.DoctorNameModelList[k].LocationDesc;
                                                model.CTPCPSMCNo = episode.DoctorNameModelList[k].CTPCPSMCNo;
                                                model.SubSpec = episode.DoctorNameModelList[k].SubSpec;
                                                model.PAADMRowId = episode.PAADMRowId;
                                                model.DoctorRowNumber = episode.DoctorNameModelListLast.Count + 1;
                                                model.DoctorDescENG = episode.DoctorNameModelList.Count > 0 ? episode.DoctorNameModelList[k].DoctorDescENG : "";
                                                episode.DoctorNameModelListLast.Add(model);
                                                CounrSameDr2 = 0;

                                            }




                                        }
                                        else
                                        {
                                            DoctorNameModel model = new DoctorNameModel();
                                            model.DoctorCode = episode.DoctorNameModelList[k].DoctorCode;
                                            model.DoctorDesc = episode.DoctorNameModelList[k].DoctorDesc;
                                            model.LocationCode = episode.DoctorNameModelList[k].LocationCode;
                                            model.LocationDesc = episode.DoctorNameModelList[k].LocationDesc;
                                            model.CTPCPSMCNo = episode.DoctorNameModelList[k].CTPCPSMCNo;
                                            model.SubSpec = episode.DoctorNameModelList[k].SubSpec;
                                            model.PAADMRowId = episode.PAADMRowId;
                                            model.DoctorRowNumber = episode.DoctorNameModelListLast.Count + 1;
                                            model.DoctorDescENG = episode.DoctorNameModelList.Count > 0 ? episode.DoctorNameModelList[k].DoctorDescENG : "";
                                            episode.DoctorNameModelListLast.Add(model);
                                        }




                                    }
                                }

                                //episode.DoctorNameModelListLast = episode.DoctorNameModelList;
                            }


                        }


                    }




                }

                else
                {

                    episode.DoctorNameModelListLast = episode.DoctorNameModelList;
                }


                // Add doctor From MedicalModel----------------------------------
                if (episode.MedicalModelDoctorList.Count > 0)
                {

                    if (episode.DoctorNameModelListLast.Count > 0)
                    {
                        int CountDR = 0;
                        for (int k = 0; k <= episode.MedicalModelDoctorList.Count - 1; k++)
                        {
                            for (int l = 0; l <= episode.DoctorNameModelListLast.Count - 1; l++)
                            {
                                if (episode.DoctorNameModelListLast[l].DoctorCode == episode.MedicalModelDoctorList[k].CTPCP_Code2)
                                {
                                    CountDR = CountDR + 1;
                                }
                            }

                            if (CountDR == 0)
                            {
                                if (!episode.MedicalModelDoctorList[k].CTPCP_Code2.Contains("NER"))
                                {
                                    DoctorNameModel model = new DoctorNameModel();
                                    model.DoctorCode = episode.MedicalModelDoctorList[k].CTPCP_Code2;
                                    model.DoctorDesc = episode.MedicalModelDoctorList[k].ORI_CPNameItemOrder;
                                    model.PAADMRowId = episode.PAADMRowId;
                                    model.LocationCode = episode.MedicalModelDoctorList[k].LocationCode;
                                    model.LocationDesc = episode.MedicalModelDoctorList[k].LocationDesc;
                                    model.SubSpec = episode.MedicalModelDoctorList[k].SubSpec;
                                    model.CTPCPSMCNo = episode.MedicalModelDoctorList[k].CTPCPSMCNo;
                                    model.DoctorDescENG = episode.MedicalModelDoctorList.Count > 0 ? episode.MedicalModelDoctorList[k].ORI_CPNameItemOrder : "";

                                    episode.DoctorNameModelListLast.Add(model);
                                    CountDR = 0;
                                }
                            }


                        }

                    }
                    else
                    {
                        for (int k = 0; k <= episode.MedicalModelDoctorList.Count - 1; k++)
                        {
                            DoctorNameModel model = new DoctorNameModel();
                            model.DoctorCode = episode.MedicalModelDoctorList[k].CTPCP_Code2;
                            model.DoctorDesc = episode.MedicalModelDoctorList[k].ORI_CPNameItemOrder;
                            model.PAADMRowId = episode.PAADMRowId;
                            model.LocationCode = episode.MedicalModelDoctorList[k].LocationCode;
                            model.LocationDesc = episode.MedicalModelDoctorList[k].LocationDesc;
                            model.SubSpec = episode.MedicalModelDoctorList[k].SubSpec;
                            model.CTPCPSMCNo = episode.MedicalModelDoctorList[k].CTPCPSMCNo;
                            model.DoctorDescENG = episode.MedicalModelDoctorList.Count > 0 ? episode.MedicalModelDoctorList[k].ORI_CPNameItemOrder : "";
                            episode.DoctorNameModelListLast.Add(model);
                        }
                        // episode.DoctorNameModelListLast = episode.MedicalModelDoctorList;
                    }


                }

                // End doctor From MedicalModel----------------------------------





                // Add doctor From DiagnosisModel----------------------------------
                if (episode.DiagnosisModelList.Count > 0)
                {
                    if (episode.DoctorNameModelListLast.Count > 0)
                    {

                        int CountDR = 0;
                        for (int k = 0; k <= episode.DiagnosisModelList.Count - 1; k++)
                        {
                            for (int l = 0; l <= episode.DoctorNameModelListLast.Count - 1; l++)
                            {
                                if (episode.DoctorNameModelListLast[l].DoctorCode == episode.DiagnosisModelList[k].ICD10DocCode)
                                {
                                    CountDR = CountDR + 1;
                                }
                            }

                            if (CountDR == 0)
                            {
                                DoctorNameModel model = new DoctorNameModel();
                                model.DoctorCode = episode.DiagnosisModelList[k].ICD10DocCode;
                                model.DoctorDesc = episode.DiagnosisModelList[k].ICD10DocName;
                                model.PAADMRowId = episode.PAADMRowId;
                                model.LocationCode = episode.DiagnosisModelList[k].LocationCode;
                                model.LocationDesc = episode.DiagnosisModelList[k].LocationDesc;
                                model.CTPCPSMCNo = episode.DiagnosisModelList[k].CTPCPSMCNo;
                                model.SubSpec = episode.DiagnosisModelList[k].SubSpec;
                                model.DoctorDescENG = episode.DiagnosisModelList.Count > 0 ? episode.DiagnosisModelList[k].ICD10DocName : "";
                                episode.DoctorNameModelListLast.Add(model);
                                CountDR = 0;
                            }


                        }

                    }
                    else
                    {

                        for (int k = 0; k <= episode.DiagnosisModelList.Count - 1; k++)
                        {
                            DoctorNameModel model = new DoctorNameModel();
                            model.DoctorCode = episode.DiagnosisModelList[k].ICD10DocCode;
                            model.DoctorDesc = episode.DiagnosisModelList[k].ICD10DocName;
                            model.PAADMRowId = episode.PAADMRowId;
                            model.LocationCode = episode.DiagnosisModelList[k].LocationCode;
                            model.LocationDesc = episode.DiagnosisModelList[k].LocationDesc;
                            model.CTPCPSMCNo = episode.DiagnosisModelList[k].CTPCPSMCNo;
                            model.SubSpec = episode.DiagnosisModelList[k].SubSpec;


                            episode.DoctorNameModelListLast.Add(model);
                        }

                    }

                }

                //End Add doctor From DiagnosisModel-

                // Find  Doctor Form CCN
                if (episode.DoctorChiefComPainNurseList.Count > 0)
                {
                    if (episode.DoctorNameModelListLast.Count > 0)
                    {

                        int CountDR = 0;
                        for (int k = 0; k <= episode.DoctorChiefComPainNurseList.Count - 1; k++)
                        {
                            for (int l = 0; l <= episode.DoctorNameModelListLast.Count - 1; l++)
                            {
                                if (episode.DoctorNameModelListLast[l].DoctorCode == episode.DoctorChiefComPainNurseList[k].DoctorCode)
                                {
                                    CountDR = CountDR + 1;
                                }
                            }

                            if (CountDR == 0)
                            {
                                DoctorNameModel model = new DoctorNameModel();
                                model.DoctorCode = episode.DoctorChiefComPainNurseList[k].DoctorCode;
                                model.DoctorDesc = episode.DoctorChiefComPainNurseList[k].DoctorName;
                                model.PAADMRowId = episode.PAADMRowId;
                                model.LocationCode = episode.DoctorChiefComPainNurseList[k].LocationCode;
                                model.LocationDesc = episode.DoctorChiefComPainNurseList[k].LocationDesc;
                                episode.DoctorNameModelListLast.Add(model);
                                CountDR = 0;
                            }


                        }
                    }
                    else
                    {

                        for (int k = 0; k <= episode.DoctorChiefComPainNurseList.Count - 1; k++)
                        {
                            DoctorNameModel model = new DoctorNameModel();
                            model.DoctorCode = episode.DoctorChiefComPainNurseList[k].DoctorCode;
                            model.DoctorDesc = episode.DoctorChiefComPainNurseList[k].DoctorName;
                            model.PAADMRowId = episode.PAADMRowId;
                            model.LocationCode = episode.DoctorChiefComPainNurseList[k].LocationCode;
                            model.LocationDesc = episode.DoctorChiefComPainNurseList[k].LocationDesc;

                            episode.DoctorNameModelListLast.Add(model);

                        }

                    }

                }

                //End CCN





                #region AddButomDoctor

                // Add doctor From   MedicalModel To Buttom ----------------------------------
                if (episode.DoctorNameModelList.Count > 0)
                {
                    if (episode.DoctorChiefComPainAllList.Count > 0)
                    {

                        int CountDR = 0;
                        for (int k = 0; k <= episode.DoctorNameModelList.Count - 1; k++)
                        {
                            for (int l = 0; l <= episode.DoctorChiefComPainAllList.Count - 1; l++)
                            {
                                if (episode.DoctorChiefComPainAllList[l].DoctorCode != episode.DoctorNameModelList[k].DoctorCode)
                                {
                                    CountDR = CountDR + 1;
                                }
                            }

                            if (CountDR != 0)
                            {
                                if (episode.DoctorNameModelList[k].DoctorCode != "0")
                                {
                                    if (episode.DoctorNameModelList[k].DoctorCode.Substring(0, 3).ToString() == "119" || episode.DoctorNameModelList[k].DoctorCode.Substring(0, 3).ToString() == "118")
                                    {

                                        DoctorChiefComPlainModel model = new DoctorChiefComPlainModel();
                                        model.DoctorCode = episode.DoctorNameModelList[k].DoctorCode;
                                        model.DoctorName = episode.DoctorNameModelList[k].DoctorDesc;
                                        model.PAADMRowId = episode.PAADMRowId;

                                        model.LocationCode = episode.DoctorNameModelList[k].LocationCode;
                                        model.LocationDesc = episode.DoctorNameModelList[k].LocationDesc;
                                        model.CTPCPSMCNo = episode.DoctorNameModelList[k].CTPCPSMCNo;
                                        model.SubSpec = episode.DoctorNameModelList[k].SubSpec;
                                        episode.DoctorChiefComPainAllList.Add(model);
                                        CountDR = 0;

                                    }
                                }
                            }

                        }
                    }
                    else
                    {
                        for (int k = 0; k <= episode.DoctorNameModelList.Count - 1; k++)
                        {
                            if (episode.DoctorNameModelList[k].DoctorCode != "0")
                            {
                                if (episode.DoctorNameModelList[k].DoctorCode.Substring(0, 3).ToString() == "119" || episode.DoctorNameModelList[k].DoctorCode.Substring(0, 3).ToString() == "118")
                                {

                                    if (episode.DoctorChiefComPainAllList.Count > 0)
                                    {

                                        int CountDR2 = 0;
                                        for (int kk = 0; kk <= episode.DoctorNameModelList.Count - 1; kk++)
                                        {
                                            for (int l = 0; l <= episode.DoctorChiefComPainAllList.Count - 1; l++)
                                            {
                                                if (episode.DoctorChiefComPainAllList[l].DoctorCode != episode.DoctorNameModelList[kk].DoctorCode)
                                                {
                                                    CountDR2 = CountDR2 + 1;
                                                }
                                            }

                                            if (CountDR2 != 0)
                                            {
                                                if (episode.DoctorNameModelList[kk].DoctorCode != "0")
                                                {
                                                    if (episode.DoctorNameModelList[kk].DoctorCode.Substring(0, 3).ToString() == "119" || episode.DoctorNameModelList[kk].DoctorCode.Substring(0, 3).ToString() == "118")
                                                    {
                                                        DoctorChiefComPlainModel model = new DoctorChiefComPlainModel();
                                                        model.DoctorCode = episode.DoctorNameModelList[kk].DoctorCode;
                                                        model.DoctorName = episode.DoctorNameModelList[kk].DoctorDesc;
                                                        model.PAADMRowId = episode.PAADMRowId;

                                                        model.LocationCode = episode.DoctorNameModelList[kk].LocationCode;
                                                        model.LocationDesc = episode.DoctorNameModelList[kk].LocationDesc;
                                                        model.CTPCPSMCNo = episode.DoctorNameModelList[kk].CTPCPSMCNo;
                                                        model.SubSpec = episode.DoctorNameModelList[kk].SubSpec;

                                                        episode.DoctorChiefComPainAllList.Add(model);
                                                        CountDR2 = 0;

                                                    }
                                                }
                                            }

                                        }





                                    }
                                    else
                                    {
                                        DoctorChiefComPlainModel model = new DoctorChiefComPlainModel();
                                        model.DoctorCode = episode.DoctorNameModelList[k].DoctorCode;
                                        model.DoctorName = episode.DoctorNameModelList[k].DoctorDesc;
                                        model.PAADMRowId = episode.PAADMRowId;

                                        model.LocationCode = episode.DoctorNameModelList[k].LocationCode;
                                        model.LocationDesc = episode.DoctorNameModelList[k].LocationDesc;
                                        model.CTPCPSMCNo = episode.DoctorNameModelList[k].CTPCPSMCNo;
                                        model.SubSpec = episode.DoctorNameModelList[k].SubSpec;

                                        episode.DoctorChiefComPainAllList.Add(model);
                                    }





                                }
                            }

                        }

                    }

                }

                // End doctor From MedicalModel To Buttom ----------------------------------


                // Add doctor From DiagnosisModel To Buttom ----------------------------------
                if (episode.DiagnosisModelList.Count > 0)
                {
                    if (episode.DoctorChiefComPainAllList.Count > 0)
                    {

                        int CountDR = 0;
                        for (int k = 0; k <= episode.DiagnosisModelList.Count - 1; k++)
                        {
                            for (int l = 0; l <= episode.DoctorChiefComPainAllList.Count - 1; l++)
                            {
                                if (episode.DoctorChiefComPainAllList[l].DoctorCode == episode.DiagnosisModelList[k].ICD10DocCode)
                                {
                                    CountDR = CountDR + 1;
                                }
                            }

                            if (CountDR == 0)
                            {
                                if (episode.DiagnosisModelList[k].ICD10DocCode != "0")
                                {
                                    if (episode.DiagnosisModelList[k].ICD10DocCode.Substring(0, 3).ToString() == "119" || episode.DiagnosisModelList[k].ICD10DocCode.Substring(0, 3).ToString() == "118")
                                    {
                                        DoctorChiefComPlainModel model = new DoctorChiefComPlainModel();
                                        model.DoctorCode = episode.DiagnosisModelList[k].ICD10DocCode;
                                        model.DoctorName = episode.DiagnosisModelList[k].ICD10DocName;
                                        model.PAADMRowId = episode.PAADMRowId;

                                        model.LocationCode = episode.DiagnosisModelList[k].LocationCode;
                                        model.LocationDesc = episode.DiagnosisModelList[k].LocationDesc;
                                        model.CTPCPSMCNo = episode.DiagnosisModelList[k].CTPCPSMCNo;
                                        model.SubSpec = episode.DiagnosisModelList[k].SubSpec;

                                        episode.DoctorChiefComPainAllList.Add(model);
                                        CountDR = 0;
                                    }
                                }
                            }

                        }
                    }
                    else
                    {
                        for (int k = 0; k <= episode.DiagnosisModelList.Count - 1; k++)
                        {
                            if (episode.DiagnosisModelList[k].ICD10DocCode != "0")
                            {
                                if (episode.DiagnosisModelList[k].ICD10DocCode.Substring(0, 3).ToString() == "119" || episode.DiagnosisModelList[k].ICD10DocCode.Substring(0, 3).ToString() == "118")
                                {

                                    if (episode.DoctorChiefComPainAllList.Count > 0)
                                    {
                                        //////
                                        int CountDRDiag = 0;
                                        for (int kD = 0; kD <= episode.DiagnosisModelList.Count - 1; kD++)
                                        {
                                            for (int lD = 0; lD <= episode.DoctorChiefComPainAllList.Count - 1; lD++)
                                            {
                                                if (episode.DoctorChiefComPainAllList[lD].DoctorCode == episode.DiagnosisModelList[kD].ICD10DocCode)
                                                {
                                                    CountDRDiag = CountDRDiag + 1;
                                                }
                                            }

                                            if (CountDRDiag == 0)
                                            {
                                                if (episode.DiagnosisModelList[kD].ICD10DocCode != "0")
                                                {
                                                    if (episode.DiagnosisModelList[kD].ICD10DocCode.Substring(0, 3).ToString() == "119" || episode.DiagnosisModelList[kD].ICD10DocCode.Substring(0, 3).ToString() == "118")
                                                    {
                                                        DoctorChiefComPlainModel model = new DoctorChiefComPlainModel();
                                                        model.DoctorCode = episode.DiagnosisModelList[kD].ICD10DocCode;
                                                        model.DoctorName = episode.DiagnosisModelList[kD].ICD10DocName;
                                                        model.PAADMRowId = episode.PAADMRowId;

                                                        model.LocationCode = episode.DiagnosisModelList[kD].LocationCode;
                                                        model.LocationDesc = episode.DiagnosisModelList[kD].LocationDesc;
                                                        model.CTPCPSMCNo = episode.DiagnosisModelList[kD].CTPCPSMCNo;
                                                        model.SubSpec = episode.DiagnosisModelList[kD].SubSpec;

                                                        episode.DoctorChiefComPainAllList.Add(model);
                                                        CountDRDiag = 0;
                                                    }
                                                }
                                            }

                                        }




                                        //////
                                    }
                                    else
                                    {

                                        DoctorChiefComPlainModel model = new DoctorChiefComPlainModel();
                                        model.DoctorCode = episode.DiagnosisModelList[k].ICD10DocCode;
                                        model.DoctorName = episode.DiagnosisModelList[k].ICD10DocName;
                                        model.PAADMRowId = episode.PAADMRowId;
                                        model.LocationCode = episode.DiagnosisModelList[k].LocationCode;
                                        model.LocationDesc = episode.DiagnosisModelList[k].LocationDesc;
                                        model.CTPCPSMCNo = episode.DiagnosisModelList[k].CTPCPSMCNo;
                                        model.SubSpec = episode.DiagnosisModelList[k].SubSpec;
                                        episode.DoctorChiefComPainAllList.Add(model);
                                    }

                                }
                            }

                        }
                    }

                }

                // End doctor From DiagnosisModel To Buttom ----------------------------------



                //Check ซ้ำ
                if (episode.DoctorChiefComPainAllList.Count > 0)
                {
                    for (int i = 0; i <= episode.DoctorChiefComPainAllList.Count - 1; i++)
                    {
                        int countAll = 0;
                        for (int j = 0; j <= episode.DoctorChiefComPainAllList.Count - 1; j++)
                        {
                            if (episode.DoctorChiefComPainAllList[i].DoctorCode == episode.DoctorChiefComPainAllList[j].DoctorCode)
                            {
                                countAll = countAll + 1;

                            }
                            if (countAll > 1)
                            {

                                episode.DoctorChiefComPainAllList.RemoveAt(j);
                                i = 0;
                                j = episode.DoctorChiefComPainAllList.Count;
                            }
                        }

                    }

                }





                #endregion //Add Doctor in buttom






                //  NotDoctorList






                #endregion
                #region SortGroupByLocation

                if (episode.DoctorNameModelListLast.Count > 0)
                {
                    for (int i = 0; i <= episode.DoctorNameModelListLast.Count - 1; i++)
                    {

                        if (episode.LocationList.Count > 0)
                        {
                            int countt = 0;
                            for (int j = 0; j <= episode.LocationList.Count - 1; j++)
                            {
                                if (episode.DoctorNameModelListLast[i].LocationCode == episode.LocationList[j].LocationCode)
                                {
                                    countt = countt + 1;
                                }

                            }
                            if (countt == 0)
                            {
                                DoctorNameModel model = new DoctorNameModel();

                                model.PAADMRowId = episode.PAADMRowId;
                                model.LocationCode = episode.DoctorNameModelListLast[i].LocationCode;
                                model.LocationDesc = episode.DoctorNameModelListLast[i].LocationDesc;
                                model.RowNumber = Convert.ToString(episode.LocationList.Count + 1);
                                episode.LocationList.Add(model);
                            }

                        }
                        else
                        {
                            DoctorNameModel model = new DoctorNameModel();

                            model.PAADMRowId = episode.PAADMRowId;
                            model.LocationCode = episode.DoctorNameModelListLast[i].LocationCode;
                            model.LocationDesc = episode.DoctorNameModelListLast[i].LocationDesc;
                            model.RowNumber = "1";
                            episode.LocationList.Add(model);
                        }



                    }

                }








                #endregion
                #region CutDortorNurse

                if (episode.DoctorNameModelListLast.Count > 0)
                {



                    int i = 0;
                    for (i = 0; i <= episode.DoctorNameModelListLast.Count - 1; i++)
                    {

                        if ((episode.DoctorNameModelListLast[i].DoctorCode != "0") && (episode.DoctorNameModelListLast[i].DoctorCode != ""))
                        {
                            if (episode.DoctorNameModelListLast[i].DoctorCode.Substring(0, 3).ToString() != "119")
                            {
                                if (episode.DoctorNameModelListLast[i].DoctorCode.Substring(0, 3).ToString() != "118")
                                {
                                    DoctorNameModel model = new DoctorNameModel();
                                    model.DoctorCode = episode.DoctorNameModelListLast[i].DoctorCode;
                                    model.DoctorDesc = episode.DoctorNameModelListLast[i].DoctorDesc;
                                    model.PAADMRowId = episode.PAADMRowId;
                                    model.LocationCode = episode.DoctorNameModelListLast[i].LocationCode;
                                    model.LocationDesc = episode.DoctorNameModelListLast[i].LocationDesc;

                                    episode.NotDoctorList.Add(model);

                                    episode.DoctorNameModelListLast.RemoveAt(i);
                                    i = -1;

                                }
                            }

                        }

                    }




                }



                #endregion
                #region CutLocation
                //หา Location ที่ไม่มีในหมอ แล้วลบออก
                if (episode.DoctorNameModelListLast.Count > 0)
                {

                    if (episode.LocationList.Count > 0)
                    {


                        int i = 0;
                        for (i = 0; i <= episode.LocationList.Count - 1; i++)
                        {
                            int j = 0;
                            int CountLoca = 0;
                            for (j = 0; j <= episode.DoctorNameModelListLast.Count - 1; j++)
                            {
                                if (episode.DoctorNameModelListLast[j].LocationCode == episode.LocationList[i].LocationCode)
                                {
                                    CountLoca = CountLoca + 1;

                                }

                            }

                            if (CountLoca == 0)
                            {

                                if (episode.DoctorChiefComPainNurseList.Count > 0)
                                {
                                    int CountNurse = 0;
                                    for (int k = 0; k <= episode.DoctorChiefComPainNurseList.Count - 1; k++)
                                    {
                                        if (episode.DoctorChiefComPainNurseList[k].LocationCode == episode.LocationList[i].LocationCode)
                                        {
                                            CountNurse = CountNurse + 1;

                                        }
                                    }

                                    if (CountNurse == 0)
                                    {
                                        episode.LocationList.RemoveAt(i);
                                        i = -1;
                                    }

                                }
                                else
                                {
                                    episode.LocationList.RemoveAt(i);
                                    i = -1;
                                }



                            }
                        }

                    }

                }
                #endregion
                #region OtherDoctor
                // Medication 

                if (episode.MedicalModelList != null)
                {
                    if (episode.MedicalModelList.Count > 0)
                    {
                        for (int j = 0; j < episode.MedicalModelList.Count - 1; j++)
                        {


                            if (episode.MedicalModelList[j].CTPCP_Code2.Substring(0, 3).ToString() != "119")
                            {
                                if (episode.MedicalModelList[j].CTPCP_Code2.Substring(0, 3).ToString() != "118")
                                {
                                    MedicationModel model = new MedicationModel();


                                    model.CTPCP_Code2 = episode.MedicalModelList[j].CTPCP_Code2;
                                    model.ARCIM_DESC = episode.MedicalModelList[j].ARCIM_DESC;
                                    model.ORI_CPNameItemOrder = episode.MedicalModelList[j].ORI_CPNameItemOrder;
                                    model.PAADM_RowID = episode.PAADMRowId;
                                    model.Dose = episode.MedicalModelList[j].Dose;
                                    model.ORI_PhQtyOrd2 = episode.MedicalModelList[j].ORI_PhQtyOrd2;
                                    model.LocationCode = episode.MedicalModelList[j].LocationCode;
                                    model.LocationDesc = episode.MedicalModelList[j].LocationDesc;
                                    episode.MedicalModelOtherList.Add(model);

                                }
                            }

                        }

                    }

                }


                // Dignosis-------------------------------------------------


                if (episode.DiagnosisModelList != null)
                {
                    if (episode.DiagnosisModelList.Count > 0)
                    {
                        for (int j = 0; j < episode.DiagnosisModelList.Count - 1; j++)
                        {

                            if (episode.DiagnosisModelList[j].ICD10DocCode.Length >= 3)
                            {
                                if (episode.DiagnosisModelList[j].ICD10DocCode.Substring(0, 3).ToString() != "119")
                                {
                                    if (episode.DiagnosisModelList[j].ICD10DocCode.Substring(0, 3).ToString() != "118")
                                    {
                                        DiagnosisModel model = new DiagnosisModel();


                                        model.ICD10DocCode = episode.DiagnosisModelList[j].ICD10DocCode;
                                        model.ICD10 = episode.DiagnosisModelList[j].ICD10;
                                        model.ICD10Desc = episode.DiagnosisModelList[j].ICD10Desc;
                                        model.icd10TypDesc = episode.DiagnosisModelList[j].icd10TypDesc;
                                        model.MRDIADesc = episode.DiagnosisModelList[j].MRDIADesc;
                                        model.LocationCode = episode.DiagnosisModelList[j].LocationCode;
                                        episode.DiagnosisModelOtherList.Add(model);

                                    }
                                }
                            }

                        }

                    }

                }



                #endregion
                #region LastDoctor
                //medication----------------------

                if (episode.NotDoctorList.Count > 0)
                {
                    int i = 0;
                    for (i = 0; i <= episode.NotDoctorList.Count - 1; i++)
                    {
                        if (episode.MedicalModelOtherList.Count > 0)
                        {
                            for (int j = 0; j <= episode.MedicalModelOtherList.Count - 1; j++)
                            {
                                if (episode.NotDoctorList[i].DoctorCode != episode.MedicalModelOtherList[j].CTPCP_Code2)
                                {
                                    if (episode.NotDoctorList.Count > 0)
                                    {
                                        int k = 0;
                                        int countM = 0;
                                        for (k = 0; k <= episode.NotDoctorList.Count - 1; k++)
                                        {
                                            if (episode.NotDoctorList[k].DoctorCode == episode.MedicalModelOtherList[j].CTPCP_Code2)
                                            {
                                                countM = countM + 1;
                                            }

                                        }

                                        if (countM == 0)
                                        {
                                            if (episode.MedicalModelOtherList[j].CTPCP_Code2.Substring(0, 3).ToString() != "119")
                                            {
                                                if (episode.MedicalModelOtherList[j].CTPCP_Code2.Substring(0, 3).ToString() != "118")
                                                {
                                                    DoctorNameModel model = new DoctorNameModel();
                                                    model.DoctorCode = episode.MedicalModelOtherList[j].CTPCP_Code2;
                                                    model.DoctorDesc = episode.MedicalModelOtherList[j].ORI_CPNameItemOrder;
                                                    model.PAADMRowId = episode.PAADMRowId;
                                                    model.LocationCode = episode.MedicalModelOtherList[j].LocationCode;
                                                    model.LocationDesc = episode.MedicalModelOtherList[j].LocationDesc;
                                                    episode.NotDoctorList.Add(model);
                                                    k = k - 1;
                                                }
                                            }
                                        }

                                    }
                                    else
                                    {
                                        if (episode.MedicalModelOtherList[j].CTPCP_Code2.Substring(0, 3).ToString() != "119")
                                        {
                                            if (episode.MedicalModelOtherList[j].CTPCP_Code2.Substring(0, 3).ToString() != "118")
                                            {
                                                DoctorNameModel model = new DoctorNameModel();
                                                model.DoctorCode = episode.MedicalModelOtherList[j].CTPCP_Code2;
                                                model.DoctorDesc = episode.MedicalModelOtherList[j].ORI_CPNameItemOrder;
                                                model.PAADMRowId = episode.PAADMRowId;
                                                model.LocationCode = episode.MedicalModelOtherList[j].LocationCode;
                                                model.LocationDesc = episode.MedicalModelOtherList[j].LocationDesc;
                                                episode.NotDoctorList.Add(model);
                                            }
                                        }
                                    }

                                }

                            }
                        }
                    }


                }




                #endregion





                //episode.OrderCategoryModelList = groupOrderToOrderCategoryAndOrderSet(summaryDao.Medication(episode.PAADMRowId));

                //Flag for update html ui by javascript
                episode.loaded = true;




                //STEP 4: response result back to client.

                return Json(new
                {
                    success = true,
                    episodeRowId = episodeRowId,
                    model = episode


                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, episodeRowId = episodeRowId, message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult getAppointment(string HN)
        {
            try
            {
                vaccineDAO vaccineDAO = new vaccineDAO();
                List<AppointModel> AppointList = vaccineDAO.getAppointmentList(HN);
                return Json(new
                {
                    success = true,
                    model = AppointList
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult GetDocscan(String HN)
        {
            try
            {
                DocscanDAO docScanDAO = new DocscanDAO();
                List<DocScanModel> DocScanList = docScanDAO.DocScanSQLServer(HN);
                return Json(new
                {
                    success = true,
                    model = DocScanList
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        public ActionResult GetQuippeScan(String HN)
        {
            try
            {
                QuippeDao quippeDAO = new QuippeDao();
                List<QuippeModel> QuippeList = quippeDAO.FindAllQuippe(HN);
                return Json(new
                {
                    success = true,
                    model = QuippeList
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        public ActionResult RediolgyListByHN(String HN)
        {
            try
            {
                SummaryDao RadioDao = new SummaryDao();
                List<RadiologyResultsModel> RediologyList = RadioDao.RadiologyResultsByHN(HN);
                return Json(new
                {
                    success = true,
                    model = RediologyList
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult DiagListByHN(String HN)
        {
            try
            {
                SummaryDao DiagDao = new SummaryDao();
                List<DiagnosisModel> DiagList = DiagDao.DiagonosisByHN(HN);
                return Json(new
                {
                    success = true,
                    model = DiagList
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult LabByHN(String HN)
        {
            try
            {
                SummaryDao LabDao = new SummaryDao();
                List<LaboratoryResultsModel> LabList = LabDao.LaboratoryResultsbyHN(HN);
                return Json(new
                {
                    success = true,
                    model = LabList
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult SQCService(String HN)
        {
            try
            {
                SummaryDao SQCDao = new SummaryDao();
                SQCModel SQC = SQCDao.SQCResult(HN);
                return Json(new
                {
                    success = true,
                    model = SQC
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        //Grouping and sort lab results.
        //Main list -> Test Set -- order by test code.
        //     Sub list -> Test code -- order by test code.
        private List<LaboratoryTestSetModel> groupLabResult(List<LaboratoryResultsModel> labList)
        {
            List<LaboratoryTestSetModel> setList = new List<LaboratoryTestSetModel>();
            if (labList == null)
            {
                return setList;
            }

            //each lab record
            foreach (LaboratoryResultsModel lab in labList)
            {
                //each test set
                bool found = false;
                foreach (LaboratoryTestSetModel set in setList)
                {
                    if (set.tsCode.Equals(lab.tsCode))
                    {
                        set.LaboratoryResultsModelList.Add(lab);

                        found = true;
                        break;
                    }
                }

                //If no test set in list, then create it and append lab to sub list.
                if (!found)
                {
                    LaboratoryTestSetModel set = new LaboratoryTestSetModel(lab.tsCode, lab.tsName);
                    setList.Add(set);
                    set.LaboratoryResultsModelList.Add(lab);
                }
            }

            setList.Sort((x, y) => x.tsCode.CompareTo(y.tsCode));

            return setList;
        }

        //Grouping order into OrderCategory and OrderSet
        //- Order category
        //     - Order date
        //          - Order Set
        //               - Order item
        private List<OrderCategoryModel> groupOrderToOrderCategoryAndOrderSet(List<MedicationModel> orderList)
        {
            List<OrderCategoryModel> orderCatList = new List<OrderCategoryModel>();
            if (orderCatList == null)
            {
                return orderCatList;
            }

            //each order item record
            foreach (MedicationModel order in orderList)
            {
                //STEP 1: find category for insert order item.===================
                //each category - all order must have category.
                String catName = categoryName(order);
                OrderCategoryModel catModel = null;
                foreach (OrderCategoryModel cat in orderCatList)
                {
                    if (cat.name.Equals(catName))
                    {
                        catModel = cat;
                        break;
                    }
                }

                //If no Category in list, then create it and append category list.
                if (catModel == null)
                {
                    catModel = new OrderCategoryModel(catName);
                    orderCatList.Add(catModel);

                    switch (catModel.name.ToLower())
                    {
                        case "medicine": catModel.position = 1; break;
                        case "lab": catModel.position = 2; break;
                        case "xray": catModel.position = 3; break;
                        default:
                            catModel.position = 1;
                            break;
                    }
                }


                //STEP 2: find date to in category's OrderDate list===============
                String dateSTR = order.ORI_SttDatSTR;
                OrderDateModel dateModel = null;
                foreach (OrderDateModel orDateModel in catModel.OrderDateModelList)
                {
                    if (orDateModel.SttDateSTR.Equals(dateSTR))
                    {
                        dateModel = orDateModel;
                        break;
                    }
                }

                //If no Date in list, then create it and append orderDate list.
                if (dateModel == null)
                {
                    dateModel = new OrderDateModel(order.ORI_SttDat);
                    catModel.OrderDateModelList.Add(dateModel);
                }


                //STEP 3: find orderSet to in category's orderSet list===============
                String orderSetDesc = order.ARCOS_Desc;
                OrderSetModel orderSetModel = null;
                foreach (OrderSetModel orSetModel in dateModel.OrderSetModelList)
                {
                    if (orSetModel.orderSetDesc.Equals(orderSetDesc))
                    {
                        orderSetModel = orSetModel;
                        break;
                    }
                }

                //If no Date in list, then create it and append orderDate list.
                if (orderSetModel == null)
                {
                    orderSetModel = new OrderSetModel(orderSetDesc);
                    dateModel.OrderSetModelList.Add(orderSetModel);
                }

                //STEP 4: append orderItem into orderSet
                orderSetModel.MedicationModelList.Add(order);

            }

            //Sort data in same date
            foreach (OrderCategoryModel cat in orderCatList)
            {
                foreach (OrderDateModel orDateModel in cat.OrderDateModelList)
                {
                    orDateModel.OrderSetModelList.Sort((x, y) => x.orderSetDesc.CompareTo(y.orderSetDesc));
                }
                cat.OrderDateModelList.Sort((x, y) => y.SttDateSTRYYYYMMDD.CompareTo(x.SttDateSTRYYYYMMDD));
            }

            orderCatList.Sort((x, y) => x.position.CompareTo(y.position));
            return orderCatList;
        }

        Dictionary<string, string> catCodeDict = new Dictionary<string, string>
        {
            { "01", "01" },
            { "0125", "0125" },
            { "01SVH", "01SVH" },
            { "01SNH", "01SNH" },
            { "01RTB", "01RTB" },
            { "01RR", "01RR" }
        };
        private String categoryName(MedicationModel order)
        {
            if (catCodeDict.ContainsKey(order.ORI_OrderCategoryCode))
            {
                return "Medicine";
            }
            else
            {
                return order.ORI_OrderCategoryDesc;
            }
        }

        public ActionResult QuippeNoteView(string p, string e)
        {
            string getUrl = ConfigurationManager.AppSettings["QuippeUrl"];
            string getCookie = new QuippeDao().getCookieByLogin();

            Response.Cookies[".QuippeDemoSiteAuth"].Value = new QuippeDao().getCookieByLogin().Split('=')[1];
            Response.Cookies[".QuippeDemoSiteAuth"].Expires = DateTime.Now.AddMinutes(1);

            //string OutUrl = getUrl + "/NoteView.htm?p=" + p + "&e=" + e;

            string OutUrl = getUrl + "/login2.htm?u=505041&p=505041&ReturnUrl=%2FNoteView.htm%3Fp%3D" + p + "%26e%3d" + e;

            ViewBag.OutUrl = OutUrl;
            ViewBag.getCookie = getCookie;

            //return View();
            return Redirect(OutUrl);
            //string ReturnScript = "<script>" +
            //    "var win = window.open('http://svh-saran-app.bdms.co.th/Quippeq4/login2.htm?u=028518172');" +
            //    //"setTimeout(function () { win.close();}, 3000);" +
            //    "window.location = '" + OutUrl + "';" +
            //    // "var win2 = window.open('" + OutUrl + "');"+
            //    // "win.close();win.close();" +
            //    "</script>";


            //return Content(ReturnScript);

        }

        [HttpPost]
        public ActionResult getObservation(string PAADMRowId)
        {
            try
            {
                SummaryDao summaryDao = new SummaryDao();
                List<ObservationModel> ObservationModelList = summaryDao.Observation(PAADMRowId);
                return Json(new
                {
                    success = true,
                    model = ObservationModelList
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult GetObservationLast(String Hn = "")
        {
            try
            {

                PatientDao patientDao = new PatientDao();
                List<ObservationModel> model = patientDao.ObservationLastEpi2(Hn);

                return Json(new { success = true, Hn = Hn, model = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Hn = Hn, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}
