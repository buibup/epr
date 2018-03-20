// Define our AngularJS application module.
var MainModule = angular.module("MainModule", []);

// Main controller for the application.
MainModule.controller(
    "OpdRecordController",
    function ($scope, $rootScope, $timeout, OpdRecordService) {

        // set defualt search form listing.  

        $scope.widthSC = screen.width;
        $scope.heightSC = screen.height;
        $scope.patientModel = {};
        $scope.episodeList = [];
        $scope.CareProvList = [];
        $scope.episodeTabList = [];
        $scope.LastOpenedRow = '';
        ResetSerch();
        $scope.SelectedDoctor = "";

        //new 
        $scope.CareProvList = [];
        $scope.StyleBtnDoctor = "btn btn-default";
        $scope.StyleBtnLocation = "btn btn-default";
        $scope.currentDoctor = "";
        $scope.Temp = "";
        $scope.currentDoctor = "";
        $scope.currentLocation = "";
        $scope.DocScan = [];
        $scope.QuippeScan = [];


        // console.log("Drug"+$scope.CheckDrug);



        //Register date picker
        $('.date').datepicker();

        // clear form
        $scope.loading = false;
        //Search();


        //$scope.selectAction = function () {
        //    $scope.searching = false;
        //    $scope.SelectedDoctorCode;
        //    $scope.patientModel.DoctorCode = $scope.SelectedDoctorCode;

        //};



        //Form action===============================================================
        function ResetSerch() {

            var hn = $scope.patientModel.PAPMI_No;
            //var DoctorCode = $scope.patientModel.DoctorCode;
            if (typeof (hn) === 'undefined')
                hn = '';

            // if (typeof (DoctorCode) === 'undefined')
            // DoctorCode = '';

            $scope.patientModel = {
                PAPMI_RowId: '',
                PAPMI_No: hn,
                Title: '',
                PAPMI_Name: '',
                PAPMI_Name2: '',
                PAPER_TelO: '',
                PAPER_TelH: '',
                FullName: '',
                MiddleName: '',
                Age: '',
                BirthDay: '',
                Gender: '',
                //  DoctorCode:DoctorCode,
                episodeList: []

            };

            $scope.episodeTabList = [];

            $scope.search_startDate = '';
            $scope.search_endDate = '';
            $scope.LastOpenedRow = '';


        }
        function Search() {

            //Prevent duplicate submit.            
            if ($scope.loading === true)
                return;
            $scope.loading = true;

            //clear content of previouse serch.
            ResetSerch();

            if ($scope.patientModel.PAPMI_No.length < 1) {
                $scope.loading = false;
                return;
            }

            //if (($scope.patientModel.PAPMI_No.length < 1) && ($scope.patientModel.DoctorCode.length < 1)) {
            //    $scope.loading = false;

            //    return;
            //}





            $scope.searching = true;

            //console.log("Begin Patient " + "loading =" + $scope.loading + "Searching =" + $scope.searching )

            //proces seaching.


            //console.log($scope.searching + " loding  " + $scope.loading);



            OpdRecordService
            //.GetPatient($scope.patientModel.PAPMI_No, $scope.patientModel.DoctorCode)
            .GetPatient($scope.patientModel.PAPMI_No)
            .then(function (data) {

                $scope.loading = false;
                $scope.searching = false;
                if (data.success) {
                    data.model.PAPMI_No = $scope.patientModel.PAPMI_No;
                    $scope.patientModel = data.model;


                    //console.log("After Patient have data" + "loading =" + $scope.loading + "Searching =" + $scope.searching)
                }
                else {

                    alert(data.message);


                }

            });

            OpdRecordService
            .GetDocscan($scope.patientModel.PAPMI_No.replace('-', ''))
            .then(function (data) {
                if (data.success) {
                    $scope.DocScan = data.model;
                } else {
                    alert(data.message);
                }
            });

            OpdRecordService
                .GetQuippeScan($scope.patientModel.PAPMI_No)
                .then(function (data) {
                    if (data.success) {
                        $scope.QuippeScan = data.model;
                    } else {
                        alert(data.message);
                    }
                });


            $scope.ObservationLastEPI = [];
            OpdRecordService
            .GetObservationLastEpi($scope.patientModel.PAPMI_No)
            .then(function (data) {
                if (data.success) {
                    $scope.ObservationLastEPI = data.model;
                } else {
                    alert(data.message);
                }
            });

            //console.log(" ff " + $scope.episodeList);
            //if ($scope.patientModel.PAADMRowId = 'undefined') {

            //    $scope.searching = false;
            //    $scope.loading = false;
            //    console.log($scope.searching + " loding  " + $scope.loading);
            //}
        }
        function IMG_Noncheck() {
            $scope.link = 'https://farm4.staticflickr.com/3261/2801924702_ffbdeda927_d.jpg';

        }
        // PRIVATE METHODS =============================================================================================
        $scope.filterByDoctor = function (epiRow, doctorCode, LocationCode) {
            filterByDoctor(epiRow, doctorCode, LocationCode);
            var epi = $("a[class='opended'] #EPI-Active").text().trim();
            if (angular.isUndefined(epi)) {
                epi = 0;
            }
            var curdocs = [], epinos = [];
            for (i = 0; i < $scope.episodeTabList.length; i++) {
                curdocs.push($scope.episodeTabList[i].curDoctorCode);
                epinos.push($scope.episodeTabList[i].EpisodeNo);
            }
            var j = epinos.indexOf(epi);
            var dr = curdocs[j];
            if ((dr === "") | (angular.isUndefined(dr))) {
                $scope.currentDoctor = "";
                $scope.StyleBtnDoctor = "btn btn-default";
                $scope.currentLocation = "";
                $scope.StyleBtnLocation = "btn btn-default";
            }

        }
        $scope.filterByDoctorEPIList = function ($scope) {
            filterEpisodeList();
        }
        $scope.QuickFilterDoctor = function () {
            $scope.StyleBtnLocation = "btn btn-default";
            $scope.currentLocation = "";
            if ($scope.currentDoctor === "") {
                $scope.currentDoctor = $scope.Temp;
                $scope.StyleBtnDoctor = "btn btn-info";
            } else {
                $scope.currentDoctor = "";
                if ($scope.Temp === "") {
                    $scope.StyleBtnDoctor = "btn btn-primary";
                } else {
                    $scope.StyleBtnDoctor = "btn btn-warning";
                }
            }
            if (($scope.Temp === "") & ($scope.currentDoctor === "")) {
                $scope.StyleBtnDoctor = "btn btn-default";
            }

        }
        $scope.QuickFilterLocation = function () {
            $scope.StyleBtnDoctor = "btn btn-default";
            $scope.currentDoctor = "";
            if ($scope.currentLocation === "") {
                $scope.currentLocation = $scope.TempLocation;
                $scope.StyleBtnLocation = "btn btn-info";
            } else {
                $scope.currentLocation = "";
                if ($scope.TempLocation === "") {
                    $scope.StyleBtnLocation = "btn btn-primary";
                } else {
                    $scope.StyleBtnLocation = "btn btn-warning";
                }
            }
            if (($scope.Temp === "") & ($scope.currentDoctor === "")) {
                $scope.StyleBtnLocation = "btn btn-default";
            }
        }
        function filterByDoctor(epiRow, doctorCode, LocationCode, doctorName) {
            $scope.Temp = doctorCode;
            $scope.TempLocation = LocationCode;

            if ($scope.Temp === "") {
                $scope.StyleBtnDoctor = "btn btn-default";

            } else {
                $scope.StyleBtnDoctor = "btn btn-warning";
                if ($scope.currentDoctor !== "") {
                    $scope.StyleBtnDoctor = "btn btn-info";
                } else {
                    $scope.StyleBtnDoctor = "btn btn-warning";
                }
            }
            if ($scope.TempLocation === "") {
                $scope.StyleBtnLocation = "btn btn-default";
            } else {
                $scope.StyleBtnLocation = "btn btn-warning";
                if ($scope.currentLocation !== "") {
                    $scope.StyleBtnLocation = "btn btn-info";
                } else {
                    $scope.StyleBtnLocation = "btn btn-warning";
                }
            }
            if (($scope.TempLocation === "") && ($scope.currentLocation !== "")) {
                $scope.StyleBtnLocation = "btn btn-info";
            } else if (($scope.TempLocation === "") && ($scope.currentLocation === "")) {
                $scope.StyleBtnLocation = "btn btn-default";
            }
            if (($scope.Temp === "") && ($scope.currentDoctor !== "")) {
                $scope.StyleBtnDoctor = "btn btn-info";
            } else if (($scope.Temp === "") && ($scope.currentDoctor === "")) {
                $scope.StyleBtnDoctor = "btn btn-default";
            }
            epiRow.curDoctorCode = doctorCode;
            epiRow.curLocationCode = LocationCode;
            filterLocationModel(epiRow, LocationCode);
            fillterDoctorNameModel(epiRow, doctorCode);
            filterNotDortor(epiRow, doctorCode);
            filterChiefComPainNurseListModel(epiRow, LocationCode);
            filterClinicalNotesModel(epiRow, doctorCode);
            filterPhyExamModel(epiRow, doctorCode);
            filterDiagnosisModel(epiRow, doctorCode);
            filterMedicalModel(epiRow, doctorCode);
            //filterEpisodeList(patientModel,doctorCode);
            //console.log("LocationCode =" + LocationCode);
            //console.log("doctorCode =" + doctorCode);              
            filterOtherMedicationModel(epiRow, doctorCode);
            filterOtherDiagnosisModel(epiRow, doctorCode);
        }
        function filterEpisodeList() {
            var epi = $("a[class='opended'] #EPI-Active").text().trim();
            var curdocs = [], epinos = [];
            for (i = 0; i < $scope.episodeTabList.length; i++) {
                curdocs.push($scope.episodeTabList[i].curDoctorCode);
                epinos.push($scope.episodeTabList[i].EpisodeNo);
            }
            var j = epinos.indexOf(epi);
            var dr = curdocs[j];
            for (i = 0; i <= $scope.episodeList.length; i++) {
                var cDoc = $scope.episodeList[i].CTPCP_Code;
            }
        }
        //filter By Location
        function filterLocationModel(epiRow, LocationCode) {
            epiRow.LocationListNew = [];
            // console.log("filterClinicalNotesModel =" + doctorCode);
            if (epiRow.LocationList != null && epiRow.LocationList.length > 0) {
                var index = 0;
                for (var i = 0; i < epiRow.LocationList.length; i++) {

                    if (LocationCode === '' || LocationCode === epiRow.LocationList[i].LocationCode) {
                        epiRow.LocationListNew[index] = epiRow.LocationList[i];
                        index++;
                    }
                }
            }
        };
        //filter By MedicalModelOtherList
        function filterOtherMedicationModel(epiRow, doctorCode) {
            epiRow.MedicalModelOtherListNew = [];
            // console.log("filterClinicalNotesModel =" + doctorCode);
            if (epiRow.MedicalModelOtherList != null && epiRow.MedicalModelOtherList.length > 0) {
                var index = 0;
                for (var i = 0; i < epiRow.MedicalModelOtherList.length; i++) {

                    if (doctorCode === '' || doctorCode === epiRow.MedicalModelOtherList[i].CTPCP_Code2) {
                        epiRow.MedicalModelOtherListNew[index] = epiRow.MedicalModelOtherList[i];
                        index++;
                    }
                }
            }
        };
        //filter By DiagnosisModelOtherList
        function filterOtherDiagnosisModel(epiRow, doctorCode) {
            epiRow.DiagnosisModelOtherListNew = [];
            // console.log("filterClinicalNotesModel =" + doctorCode);
            if (epiRow.DiagnosisModelOtherList != null && epiRow.DiagnosisModelOtherList.length > 0) {
                var index = 0;
                for (var i = 0; i < epiRow.DiagnosisModelOtherList.length; i++) {

                    if (doctorCode === '' || doctorCode === epiRow.DiagnosisModelOtherList[i].CTPCP_Code2) {
                        epiRow.DiagnosisModelOtherListNew[index] = epiRow.DiagnosisModelOtherList[i];
                        index++;
                    }
                }
            }
        };
        //filter Chifcompain
        function filterClinicalNotesModel(epiRow, doctorCode) {
            epiRow.ClinicalNotesModelList2 = [];
            // console.log("filterClinicalNotesModel =" + doctorCode);
            if (epiRow.ClinicalNotesModelList != null && epiRow.ClinicalNotesModelList.length > 0 && epiRow.chkCC == 'Y') {
                var index = 0;
                for (var i = 0; i < epiRow.ClinicalNotesModelList.length; i++) {

                    if (doctorCode === '' || doctorCode === epiRow.ClinicalNotesModelList[i].notNurseIdCode) {
                        epiRow.ClinicalNotesModelList2[index] = epiRow.ClinicalNotesModelList[i];
                        index++;
                    }
                }
            }
        };
        //filter Procedures
        function filterProceduresModel(epiRow, doctorCode) {
            epiRow.ProceduresModelListNew = [];
            // console.log("filterProceduresModel =" + doctorCode);
            if (epiRow.ProceduresModelList != null && epiRow.ProceduresModelList.length > 0) {
                var index = 0;
                for (var i = 0; i < epiRow.ProceduresModelList.length; i++) {

                    if (doctorCode === '' || doctorCode === epiRow.ProceduresModelList[i].OperCpCode) {
                        epiRow.ProceduresModelListNew[index] = epiRow.ProceduresModelList[i];
                        index++;
                    }
                }
            }
        };
        //fiterDictor
        function fillterDoctorNameModel(epiRow, doctorCode) {
            epiRow.DoctorNameModelListLNew = [];
            //  console.log("DoctorNameModelList =" + doctorCode);
            if (epiRow.DoctorNameModelListLast != null && epiRow.DoctorNameModelListLast.length > 0) {
                var index = 0;
                for (var i = 0; i < epiRow.DoctorNameModelListLast.length; i++) {

                    if (doctorCode === '' || doctorCode === epiRow.DoctorNameModelListLast[i].DoctorCode) {
                        epiRow.DoctorNameModelListLNew[index] = epiRow.DoctorNameModelListLast[i];
                        index++;
                    }
                }
            }
        };
        //filter People not Doctor
        function filterNotDortor(epiRow, doctorCode) {
            epiRow.NotDoctorListNew = [];
            //console.log("filterPhyExamModel =" + doctorCode);
            if (epiRow.NotDoctorList != null && epiRow.NotDoctorList.length > 0) {
                var index = 0;
                for (var i = 0; i < epiRow.NotDoctorList.length; i++) {

                    if (doctorCode === '' || doctorCode === epiRow.NotDoctorList[i].DoctorCode) {
                        epiRow.NotDoctorListNew[index] = epiRow.NotDoctorList[i];
                        index++;
                    }
                }
            }
        };
        //filter PhyExamModelList
        function filterPhyExamModel(epiRow, doctorCode) {
            epiRow.PhyExamModelList2 = [];
            //console.log("filterPhyExamModel =" + doctorCode);
            if (epiRow.PhyExamModelList != null && epiRow.PhyExamModelList.length > 0 && epiRow.chkPhyExam == 'Y') {
                var index = 0;
                for (var i = 0; i < epiRow.PhyExamModelList.length; i++) {

                    if (doctorCode === '' || doctorCode === epiRow.PhyExamModelList[i].DoctorCode) {
                        epiRow.PhyExamModelList2[index] = epiRow.PhyExamModelList[i];
                        index++;
                    }
                }
            }
        };
        //filter Diagnosis
        function filterDiagnosisModel(epiRow, doctorCode) {
            epiRow.DiagnosisModelListNew = [];
            // console.log("filterDiagnosisModel =" + doctorCode);
            if (epiRow.DiagnosisModelList != null && epiRow.DiagnosisModelList.length > 0 && epiRow.chkDiag == 'Y') {
                var index = 0;
                for (var i = 0; i < epiRow.DiagnosisModelList.length; i++) {

                    if (doctorCode === '' || doctorCode === epiRow.DiagnosisModelList[i].ICD10DocCode) {
                        epiRow.DiagnosisModelListNew[index] = epiRow.DiagnosisModelList[i];
                        index++;
                    }
                }
            }
        };
        //filter DoctorChiefComPainNurseList
        function filterChiefComPainNurseListModel(epiRow, LocationCode) {
            epiRow.DoctorChiefComPainNurseListNew = [];
            // console.log("filterDiagnosisModel =" + doctorCode);
            if (epiRow.DoctorChiefComPainNurseList != null && epiRow.DoctorChiefComPainNurseList.length > 0 && epiRow.chkDiag == 'Y') {
                var index = 0;
                for (var i = 0; i < epiRow.DoctorChiefComPainNurseList.length; i++) {

                    if (LocationCode === '' || LocationCode === epiRow.DoctorChiefComPainNurseList[i].LocationCode) {
                        epiRow.DoctorChiefComPainNurseListNew[index] = epiRow.DoctorChiefComPainNurseList[i];
                        index++;
                    }
                }
            }
        };
        //filter Medication
        function filterMedicalModel(epiRow, doctorCode) {
            epiRow.MedicalModelListNew = [];
            //console.log("filterMedicalModel =" + doctorCode);
            if (epiRow.MedicalModelList != null && epiRow.MedicalModelList.length > 0) {
                var index = 0;
                for (var i = 0; i < epiRow.MedicalModelList.length; i++) {

                    if (doctorCode === '' || doctorCode === epiRow.MedicalModelList[i].CTPCP_Code2) {
                        epiRow.MedicalModelListNew[index] = epiRow.MedicalModelList[i];
                        //   console.log(index);
                        index++;
                        // console.log("last "+index);
                    }

                }

                //  console.log("lll"+ epiRow.MedicalModelListNew.length);


            }

            //console.log(epiRow.MedicalModelListNew.length);
        };
        // Check  Box
        //$scope.showJson = function () {
        //    $scope.json = angular.toJson($scope.patientModel);

        //}
        //open link
        function openWindow(url) {
            window.open(url, '_blank');
            window.focus();
        }
        $scope.openWindow = function () {
            var url = "http://10.105.11.21/SNHDocview/main.aspx?hn=" + $scope.patientModel.PAPMI_No;
            openWindow(url);
        }
        $scope.openWindowSVH = function () {
            var url = "http://10.104.11.21/SVHDocview/main.aspx?hn=" + $scope.patientModel.PAPMI_No;
            openWindow(url);

        }
        $scope.openWindowPatho = function () {
            var Papmino = $scope.patientModel.PAPMI_No.replace('-', '');
            var Papmino2 = Papmino.replace('-', '');
            var url = "http://10.105.10.18/asp/samitivej/patho/browse/_case.asp?HN=" + Papmino2 + "&name_hn=" + Papmino2;
            openWindow(url);
        }
        $scope.openWindowPrint = function () {
            //var epi = angular.element(document.getElementById("EPI-Active").innerHTML.trim().substring(0, 13));
            var epi = $("a[class='opended'] #EPI-Active").text().trim();
            var curdocs = [], epinos = [];
            for (i = 0; i < $scope.episodeTabList.length; i++) {
                curdocs.push($scope.episodeTabList[i].curDoctorCode);
                epinos.push($scope.episodeTabList[i].EpisodeNo);
            }
            var j = epinos.indexOf(epi);
            var dr = curdocs[j];
            var Papmino = $scope.patientModel.PAPMI_No.replace('-', '');
            var Papmino2 = Papmino.replace('-', '');
            var docName = angular.element(document.getElementsByClassName("headerConDR"));
            var url = "http://10.105.10.7/epr2/printform/index.html?hn=" + $scope.patientModel.PAPMI_No + "&epi=" + epi + "&dr=" + dr + "&" + window.location.search.split("&")[2] + "#/forms";
           // var url = "http://10.104.10.45/eprsnh_dome/printform/index.html?hn=" + $scope.patientModel.PAPMI_No + "&epi=" + epi + "&dr=" + dr + "&" + window.location.search.split("&")[2] + "#/forms";

            openWindow(url);
        }
        $scope.openWindowChart = function () {
            var url = "http://10.104.10.44/eReport/Reports/R0001.aspx?rowid=" + $scope.patientModel.PAPMI_RowId;
            openWindow(url);
        }
        $scope.openWindowGiendo = function () {
            var hn = "";
            if ($scope.patientModel.PAPMI_No.length == 10) {
                hn = $scope.patientModel.PAPMI_No.substr(0, 2) + '-' + $scope.patientModel.PAPMI_No.substr(2, 2) + '-' + $scope.patientModel.PAPMI_No.substr(4, 9);
            } else {
                hn = $scope.patientModel.PAPMI_No;
            }
            var url = "/EPRSNHTEST/GIENDO?a=a&hn=" + hn;
            openWindow(url);
        }
        $scope.openWindowVaccine = function () {
            var hn = "";
            if ($scope.patientModel.PAPMI_No.length == 10) {
                hn = $scope.patientModel.PAPMI_No.substr(0, 2) + '-' + $scope.patientModel.PAPMI_No.substr(2, 2) + '-' + $scope.patientModel.PAPMI_No.substr(4, 9);
            } else {
                hn = $scope.patientModel.PAPMI_No;
            }
            var url = "http://10.105.10.7:8787/?hn=" + hn;
            openWindow(url);
        }
        $scope.openWindowSQC = function () {
            var hn = "";
            if ($scope.patientModel.PAPMI_No.length == 10) {
                hn = $scope.patientModel.PAPMI_No.substr(0, 2) + '-' + $scope.patientModel.PAPMI_No.substr(2, 2) + '-' + $scope.patientModel.PAPMI_No.substr(4, 9);
            } else {
                hn = $scope.patientModel.PAPMI_No;
            }
            var sex, height, weight, BMI = "";

            $scope.patientModel.Gender.match("M") ? sex = "M" : sex = "F";
            $scope.ObservationLastEPI[0].height.match("N/A") ? height = "0" : height = $scope.ObservationLastEPI[0].height;
            $scope.ObservationLastEPI[0].weight.match("N/A") ? weight = "0" : weight = $scope.ObservationLastEPI[0].weight;
            $scope.ObservationLastEPI[0].BMI.match("N/A") ? BMI = "0" : BMI = $scope.ObservationLastEPI[0].BMI;

            var url = "http://tsv.samitivej.co.th/qualitycare_test/IE7Print_NewMainPage50_Male.aspx?NameUser=02800002&HN=" + hn + "&NAME=" + $scope.patientModel.FullName + "&AGE=" + $scope.patientModel.Age + "&SWE1=" + sex + "&HT=" + height + "&BW=" + weight + "&BMI=" + BMI;
            var url = "http://10.105.10.7/sec/?age=" + $scope.patientModel.Age + "&sex=" + sex + "&HN=" + hn;
            openWindow(url);
        }
        $scope.openWindowCheckUp = function () {
            var hn = "";
            if ($scope.patientModel.PAPMI_No.length == 10) {
                hn = $scope.patientModel.PAPMI_No.substr(0, 2) + '-' + $scope.patientModel.PAPMI_No.substr(2, 2) + '-' + $scope.patientModel.PAPMI_No.substr(4, 9);
            } else {
                hn = $scope.patientModel.PAPMI_No;
            }
            var url = "http://10.105.10.92/CheckupSVHWeb/LaunchWeb.html?HN=" + hn;
            openWindow(url);
        }



        $scope.showPhysicianList = function (epiModel) {
            if (epiModel.showPhyList)
                epiModel.showPhyList = false;
            else
                epiModel.showPhyList = true;
        };
        $scope.$watch('$OpdRecordController', function () {
            Search();
        });
        $scope.SeachPatient = function () {
            Search();
        };
        $scope.Login = function () {
            alet('555');
        }
        $scope.loadEpisodeMedical = function (episodeModel) {
            var temp = [];

           
            //ensure the code will be called in a single $apply block.
            $timeout(function () {

                //CASE 1: If episode in list then, only active episode tab only.
                //CASE 2: If loading episodeMedical, then waiting for response.
                var found = false;
                var epi;
                var len = $scope.episodeTabList.length;

                for (var i = 0; i < len; i++) {
                    epi = $scope.episodeTabList[i];
                    if (epi.EpisodeNo === episodeModel.EpisodeNo) {
                        //episodeModel.marked = epi.marked;
                        epi.active = true;
                        found = true;
                    }
                    else {
                        epi.active = false;
                    }
                }
                if (found) {
                    temp = $scope.episodeTabList;
                    return;
                }

                //CASE 3: If is not in list, then load episode and append to episodeMedical listing with active tab.

                episodeModel.active = true;
                removeUnmarkTab();

                $scope.episodeTabList.push(episodeModel); // open new Tab

                //console.log(episodeModel);
                //$scope.episodeTabList = [episodeModel];     // load Old tab

                //force update 'active' attribute on ui by clear object and reder in next $scope.$apply();
                temp = $scope.episodeTabList;
                $scope.episodeTabList = [];
                $("#DocScan").value(episodeModel.EpisodeNo);
            });

            //force update 'active' attribute on ui by clear object and reder in next $scope.$apply();
            $timeout(function () {
                $scope.episodeTabList = temp;
            });

            //Load episode's summary data.
            if (typeof (episodeModel) !== 'undefined' && episodeModel.loaded === false) {

                OpdRecordService
                .GetSummary(episodeModel.PAADMRowId)
                .then(function (data) {
                    $scope.loading = false;
                    if (data.success) {
                        //console.log(data.model);
                        $scope.json = angular.toJson(data.model);

                        // return angular.fromJson(angular.toJson(data.model));
                        var loadedEpi = data.model;
                        //set record infor back to episode list.
                        var len = $scope.episodeList.length;
                        for (var i = 0; i < len; i++) {
                            if ($scope.episodeList[i].EpisodeNo === loadedEpi.EpisodeNo) {
                                $scope.episodeList[i] = loadedEpi;
                                break;
                            }
                        }
                        //set record infor back to episode list.
                        var lenTab = $scope.episodeTabList.length;

                        for (var i = 0; i < lenTab; i++) {
                            if ($scope.episodeTabList[i].EpisodeNo === loadedEpi.EpisodeNo) {
                                loadedEpi.active = $scope.episodeTabList[i].active;
                                loadedEpi.marked = $scope.episodeTabList[i].marked;
                                loadedEpi.ERowid = $scope.episodeTabList[i].ERowid;
                                $scope.episodeTabList[i] = loadedEpi;

                                break;
                            }
                        }
                        filterByDoctor(loadedEpi, '', '');
                    }
                    else {
                        alert(data.message);
                        lenTab = $scope.episodeTabList.length;
                        for (var i = 0; i < lenTab; i++) {
                            if ($scope.episodeTabList[i].PAADMRowId === data.episodeRowId) {
                                $scope.episodeTabList[i].loaded = true;
                                $scope.episodeTabList[i].hasError = true;
                                break;
                            }
                        }
                    }
                });


            }

        };
        function removeUnmarkTab() {
            if ($scope.episodeTabList.length < 1)
                return;

            for (var i = 0; i < $scope.episodeTabList.length; i++) {
                epi = $scope.episodeTabList[i];
                if (epi.marked === false) {
                    $scope.episodeTabList.splice(i, 1);
                    i--; //reduce index to current element.
                }
            }
        }
        $scope.MarkEisodeTabMedical = function (episotedTab) { //SET Mark
            if (episotedTab.marked === true) {
                episotedTab.marked = false;

            }
            else {
                episotedTab.marked = true;
            }
        }
        // key down------------
        //$scope.myFunct = function (keyEvent) {
        //    if (keyEvent.which === 13 || keyEvent.which === 40 || keyEvent.which === 38)
        //        console.log('Im a lert');
        //}
        $scope.closeEisodeTabMedical = function (episotedTab) {
            //ensure the code will be called in a single $apply block.
            var temp = [];
            $timeout(function () {

                episotedTab.marked = false;
                var epi;
                var len = $scope.episodeTabList.length;

                for (var i = 0; i < len; i++) {
                    epi = $scope.episodeTabList[i];
                    epi.active = false;
                    if (epi.EpisodeNo === episotedTab.EpisodeNo) {
                        $scope.episodeTabList.splice(i, 1);
                        i--; //reduce index to current element.
                    }
                    len = $scope.episodeTabList.length;
                }

                //active lasted tab.
                if ($scope.episodeTabList.length > 0) {
                    epi = $scope.episodeTabList[$scope.episodeTabList.length - 1];
                    epi.active = true;
                }

                //force update 'active' attribute on ui by clear object and reder in next $scope.$apply();
                temp = $scope.episodeTabList;
                $scope.episodeTabList = [];

            });

            //force update 'active' attribute on ui by clear object and reder in next $scope.$apply();
            $timeout(function () {
                $scope.episodeTabList = temp;
            });

        };
        $scope.closeAllEisodeTabMedical = function () {
            $scope.episodeTabList = [];
        }
        // PRIVATE METHODS =============================================================================================
        $scope.orderCategoryList = {
            '01': '01',
            '0125': '0125',
            '01SVH': '01SVH',
            '01SNH': '01SNH',
            '01RTB': '01RTB',
            '01RR': '01RR'
        };
        $scope.OrderGroupName = function (medModel) {
            if ($scope.orderCategoryList[medModel.ORI_OrderCategoryCode] !== undefined) {
                return "Medicine";
            }
            else {
                return medModel.ORI_OrderCategoryDesc;
            }
        }
        $scope.clinicalNoteCodeList = {
            '02': '02',
            'PN': 'PN',
            'TP': 'TP'
        };
        $scope.isClinicalNote = function (NOTClinNoteTypeCode) {
            if ($scope.clinicalNoteCodeList[NOTClinNoteTypeCode] !== undefined) {

                return true;
            }
            else {
                return false;
            }
        }
        $scope.createConsultText = function (consultationOrderModel) {
            //STEP 1: find speci value
            var txtSepeci = "";
            if (consultationOrderModel.QSpecified !== "   ") {
                if (consultationOrderModel.QSpecified === "Specified Doctor Name") {
                    txtSepeci = "Specified Doctor : " + consultationOrderModel.cpDesc;
                }
                else {
                    txtSepeci = consultationOrderModel.QSpecified;
                }
            }
            else {
                if (consultationOrderModel.QNotSpeci === "1") {
                    txtSepeci = "Not Specified Doctor";
                }
                else if (consultationOrderModel.QSpeci === "1") {
                    txtSepeci = "Specified Doctor : " + consultationOrderModel.cpDesc;
                }
            }

            var convert_date = "";
            if (consultationOrderModel.QDate !== null && consultationOrderModel.QDate !== '') {
                var d = new Date(consultationOrderModel.QDate);
                convert_date = d.toString("dd/MMM/yyyy");
            }

            var txtUrgent = "";
            if (consultationOrderModel.QUrgentCb !== "   ") {
                if (consultationOrderModel.QUrgentCb = "OPD Appointment  Date") {
                    txtUrgent = "OPD Appointment Date : ";
                    if (consultationOrderModel.QDate !== null && consultationOrderModel.QDate !== "")
                        txtUrgent += convert_date
                }
                else {
                    txtUrgent = consultationOrderModel.QUrgentCb;
                }
            }
            else {
                if (consultationOrderModel.QUrgent === "1")
                    txtUrgent = "Urgent (Within 3 hours)";
                else if (consultationOrderModel.QNotUrgent === "1")
                    txtUrgent = "Not urgent (Within 48 hours)";
                else if (consultationOrderModel.QOPDAppoin === "1") {
                    txtUrgent = "OPD Appointment Date : ";
                    if (consultationOrderModel.QDate !== null && consultationOrderModel.QDate !== "")
                        txtUrgent += convert_date;
                }
            }

            var txtConsult = "";
            if (txtSepeci !== "") {
                txtConsult = txtSepeci;
                if (txtUrgent !== "")
                    txtConsult += ", " + txtUrgent;
            }
            else if (txtUrgent !== "") {
                txtConsult = txtUrgent;
            }

            return txtConsult;
        };
        $scope.createBPTxt = function (observRow) {
            var bpReturnTxt = "";
            if (observRow.BPSystolic !== null && observRow.BPSystolic !== '')
                bpReturnTxt += observRow.BPSystolic;

            bpReturnTxt += "/";

            if (observRow.BPDiastolic !== null && observRow.BPDiastolic !== '')
                bpReturnTxt += observRow.BPDiastolic;

            return bpReturnTxt;
        };
        //-------------------------------------------
        $scope.peopleArray = [
          { id: "1", firstName: "John", lastName: "Doe", sex: "M" },
          { id: "2", firstName: "Alice", lastName: "White", sex: "F" },
          { id: "3", firstName: "Michael", lastName: "Green", sex: "M" }
        ];
        // dropdownlist
        //$scope.CareProvList = data.CareProv;
        //console.log($scope.CareProvList);
        $scope.getPersonFullName = function (person) {
            return person.firstName + " " + person.lastName;
        };
        $scope.getPersonIdAndFullName = function (person) {
            return "(" + person.id + ") " + person.firstName + " " + person.lastName;
        };
        $scope.selectPersonById = function (id) {
            $scope.peopleArrayValue5 = { id: id };
        };
        //----------------------------------------------
        //Highlight
        $scope.LastOpenedRow = null;
        $scope.activeLastOpenedRow = function (rowIndex) {
            $scope.LastOpenedRow = rowIndex;
        }
        $scope.keydown = function (e, rowIndex) {
            e = e || window.event;
            if (e.keyCode == '38') {
                e.preventDefault();
                if ($scope.LastOpenedRow !== 0) {
                    var newIndex = $scope.LastOpenedRow - 1;
                    $scope.loadEpisodeMedical($scope.patientModel.episodeList[newIndex]);
                    $scope.activeLastOpenedRow(newIndex);

                }
            }
            else if (e.keyCode == '40') {
                e.preventDefault();
                if ($scope.LastOpenedRow < $scope.patientModel.episodeList.length - 1) {
                    var newIndex = $scope.LastOpenedRow + 1;
                    $scope.loadEpisodeMedical($scope.patientModel.episodeList[newIndex]);
                    $scope.activeLastOpenedRow(newIndex);
                }
            }
        }//end function
    }
);
MainModule.filter("docEPIfilter", function () {
    return function (items, doc) {
        var result = [];
        if (!angular.isUndefined(doc) && !(doc === "")) {
            for (var i = 0; i < items.length; i++) {
                for (var j = 0; j < items[i].DiagnosisModelList.length; j++) {
                    var cDoc = items[i].DiagnosisModelList[j].ICD10DocCode;
                    if (cDoc === doc) {
                        result.push(items[i]);
                        j = items[i].DiagnosisModelList.length;
                    }
                }
            }
        } else {
            return items;
        }
        return result;
    };
});
MainModule.filter("docEpifilterLocaion", function () {
    return function (items, loc) {
        var result = [];
        if (!angular.isUndefined(loc) && !(loc === "")) {
            for (var i = 0; i < items.length; i++) {
                for (var j = 0; j < items[i].DiagnosisModelList.length; j++) {
                    var cLoc = items[i].DiagnosisModelList[j].LocationCode.trim();
                    if (cLoc === loc) {
                        result.push(items[i]);
                        j = items[i].DiagnosisModelList.length;
                    }
                }
            }
        } else {
            return items;
        }
        return result;
    };
});
MainModule.filter("dateRangefilter", function () {
    return function (items, from, to) {
        var df = parseDate(from);
        var dt = parseDate(to);
        var result = [];

        if (df > 0 || dt > 0) {
            for (var i = 0; i < items.length; i++) {
                var cdate = parseDate(items[i].EpiDateString);
                if ((df === 0 || cdate >= df) && (dt === 0 || cdate <= dt)) {
                    result.push(items[i]);
                }
            }
            return result;
        }
        else {
            return items;
        }
    };
});
MainModule.filter("filterepi", function () {
    return function (input, scope) {
        var condition = scope.epiRow.EpisodeNo.replace(/-/gi, '');
        var outArr = [];
        angular.forEach(input, function (item) {
            if (item.episode == condition) {
                outArr.push(item);
                //console.log(item);
            }
        })
        //console.log(scope.epiRow.EpisodeNo.replace(/-/gi,''));
        //console.log(input);
        //return input.replace(/-/gi, '');
        //console.log(outArr);
        return (outArr);
    }
})

MainModule.filter("filterQuippe", function () {
    return function (input, scope) {
        //console.log(scope.epiRow.EpisodeNo);
        var condition = scope.epiRow.EpisodeNo;
        //console.log(condition);

        var outArr = [];
        angular.forEach(input, function (item) {
            
            var codeTemp = item.code.replace(/-/g, '');
            var conditionTemp = condition.replace(/-/g, '');

            if (codeTemp == conditionTemp) {
                outArr.push(item);
            }
        })
        return (outArr);
    }
})

function parseDate(ddSmmSyyyy) {
    if (typeof (ddSmmSyyyy) === 'undefined' || ddSmmSyyyy === '')
        return 0;
    var parts = ddSmmSyyyy.split('/');
    var cdate = new Date(parts[2], parts[1] - 1, parts[0]);
    var time = cdate.getTime();
    return time;
}
MainModule.filter('beginLine', function () {
    return function (input) {
        return input.replace(/\n/, '').replace(/\n/, '').trim().replace(/###/gi, '\n');
    };
});
