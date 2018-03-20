/**
 * Created by tong on 2015-07-03.
 */
function formdata(){
  return fdata;
}

var fdata=
[
  {
    "formname": "OPD ASSESSMENT FORM (ADULT)",
    "ref": "12-MRF-435/01",
    "headTpltNo":0,
    "page":[
        {
        "pageno":1,
        "img":"12MRF435_01.xps.1.png",
        "position":{
            "dem": {
                "name": {"x": 100, "y": 50},

                "hn": {"x": 100, "y": 55},
                "dob": {"x": 150, "y": 55},
                "age": {"x": 180, "y": 55},

                "dept": {"x": 100, "y": 60},
                "date": {"x": 150, "y": 60},
                "time": {"x": 190, "y": 60},

                "phys": {"x": 100, "y": 65},
                "alg": {"nka": {"x": 100, "y": 55}, "text": {"x": 100, "y": 55}},
                "food": {"nka": {"x": 100, "y": 55}, "text": {"x": 100, "y": 55}},
                "other": {"x": 190, "y": 60}
            },
            "scrn":{
                "status":{"walk":{"x": 190, "y": 60},"wheel":{"x": 190, "y": 60},"stretch":{"x": 190, "y": 60}},
                "bw":{"x": 190, "y": 60},
                "ht":{"x": 190, "y": 60},
                "bmi":{"x": 190, "y": 60},
                "ccn":{"x": 190, "y": 60},
                "obs":{"bt":{"x": 190, "y": 60},"sbp":{"x": 190, "y": 60},"dbp":{"x": 190, "y": 60},"pr":{"x": 190, "y": 60},"rr":{"x": 190, "y": 60},"sat":{"x": 190, "y": 60}},
                "pain":{
                    "score":{"x": 190, "y": 60},
                    "loc":{"x": 190, "y": 60},
                    "assess":{"nrs":{"x": 190, "y": 60},"frs":{"x": 190, "y": 60}},"flacc":{"x": 190, "y": 60},"nps":{"x": 190, "y": 60},"bps":{"x": 190, "y": 60},"pps":{"x": 190, "y": 60}},
                "fall":{"no":{"x": 190, "y": 60},"yes":{"x": 190, "y": 60},"area":{"x": 190, "y": 60}},
                "abuse":{"no":{"x": 190, "y": 60},"yes":{"x": 190, "y": 60}},
                "psycho":{"neg":{"x": 190, "y": 60},"pos":{"x": 190, "y": 60}},
                "triage":{"emr":{"x": 190, "y": 60},"urg":{"x": 190, "y": 60},"nur":{"x": 190, "y": 60}},
                "assessor":{"x": 190, "y": 60}

            }
        }
    },
        {
            "pageno":2,
            "img":"12MRF435_01.xps.2.png",
            "position":{
                "dem": {
                    "name": {"x": 100, "y": 50},

                    "hn": {"x": 100, "y": 55},
                    "dob": {"x": 150, "y": 55},
                    "age": {"x": 180, "y": 55},

                    "dept": {"x": 100, "y": 60},
                    "date": {"x": 150, "y": 60},
                    "time": {"x": 190, "y": 60},

                    "phys": {"x": 100, "y": 65},
                    "alg": {"nka": {"x": 100, "y": 55}, "text": {"x": 100, "y": 55}},
                    "food": {"nka": {"x": 100, "y": 55}, "text": {"x": 100, "y": 55}},
                    "other": {"x": 190, "y": 60}
                },
                "scrn":{
                    "status":{"walk":{"x": 190, "y": 60},"wheel":{"x": 190, "y": 60},"stretch":{"x": 190, "y": 60}},
                    "bw":{"x": 190, "y": 60},
                    "ht":{"x": 190, "y": 60},
                    "bmi":{"x": 190, "y": 60},
                    "ccn":{"x": 190, "y": 60},
                    "obs":{"bt":{"x": 190, "y": 60},"sbp":{"x": 190, "y": 60},"dbp":{"x": 190, "y": 60},"pr":{"x": 190, "y": 60},"rr":{"x": 190, "y": 60},"sat":{"x": 190, "y": 60}},
                    "pain":{
                        "score":{"x": 190, "y": 60},
                        "loc":{"x": 190, "y": 60},
                        "assess":{"nrs":{"x": 190, "y": 60},"frs":{"x": 190, "y": 60}},"flacc":{"x": 190, "y": 60},"nps":{"x": 190, "y": 60},"bps":{"x": 190, "y": 60},"pps":{"x": 190, "y": 60}},
                    "fall":{"no":{"x": 190, "y": 60},"yes":{"x": 190, "y": 60},"area":{"x": 190, "y": 60}},
                    "abuse":{"no":{"x": 190, "y": 60},"yes":{"x": 190, "y": 60}},
                    "psycho":{"neg":{"x": 190, "y": 60},"pos":{"x": 190, "y": 60}},
                    "triage":{"emr":{"x": 190, "y": 60},"urg":{"x": 190, "y": 60},"nur":{"x": 190, "y": 60}},
                    "assessor":{"x": 190, "y": 60}

                }
            }
        }
    ]
  },
  {
    "formname": "ADMISSION NOTE",
    "ref": "12-MRF-226/02",
    "template":"<table><tr><td colspan='3' style='padding-left:1cm;' id='dName'>name</td></tr>"+
    "<tr><td colspan='3' style='padding-left:1cm;' id='dHN'>{{hn}}</td></tr>"+
    "<tr><td style='padding-left:1.7cm;width:1.9cm;' id='dDOB'>{{dob}}</td><td style='padding-left:1.0cm;width:1.5cm;' id='dAge'>{{age}}</td><td style='padding-left:1.0cm;' id='dSex'>{{sex}}</td></tr>"+
    "<tr><td style='padding-left:1cm;' id='dWard'>{{ward}}</td><td style='padding-left:1.5cm;' id='dRoom'>{{room}}</td><td>&nbsp;</td></tr>"+
    "<tr><td colspan='3' style='padding-left:2.5cm;' id='dAttn'>{{attndr}}</td></tr></table>",
    "page":[
      {
        "pageno":1,
        "img":"Admission Note (Gen).xps.1.png",
        "position":{
          "dtemplate":{
            "style":"position:relative;left:11.6cm; top:-29.5cm; z-index:1; width: 433px; height: 144px;"
          },
          "dem": {
            "name": {"x": 100, "y": 50},

            "hn": {"x": 100, "y": 55},
            "dob": {"x": 150, "y": 55},
            "age": {"x": 180, "y": 55},

            "dept": {"x": 100, "y": 60},
            "date": {"x": 150, "y": 60},
            "time": {"x": 190, "y": 60},

            "phys": {"x": 100, "y": 65},
            "alg": {"nka": {"x": 100, "y": 55}, "text": {"x": 100, "y": 55}},
            "food": {"nka": {"x": 100, "y": 55}, "text": {"x": 100, "y": 55}},
            "other": {"x": 190, "y": 60}
          },
          "scrn":{
            "status":{"walk":{"x": 190, "y": 60},"wheel":{"x": 190, "y": 60},"stretch":{"x": 190, "y": 60}},
            "bw":{"x": 190, "y": 60},
            "ht":{"x": 190, "y": 60},
            "bmi":{"x": 190, "y": 60},
            "ccn":{"x": 190, "y": 60},
            "obs":{"bt":{"x": 190, "y": 60},"sbp":{"x": 190, "y": 60},"dbp":{"x": 190, "y": 60},"pr":{"x": 190, "y": 60},"rr":{"x": 190, "y": 60},"sat":{"x": 190, "y": 60}},
            "pain":{
              "score":{"x": 190, "y": 60},
              "loc":{"x": 190, "y": 60},
              "assess":{"nrs":{"x": 190, "y": 60},"frs":{"x": 190, "y": 60}},"flacc":{"x": 190, "y": 60},"nps":{"x": 190, "y": 60},"bps":{"x": 190, "y": 60},"pps":{"x": 190, "y": 60}},
            "fall":{"no":{"x": 190, "y": 60},"yes":{"x": 190, "y": 60},"area":{"x": 190, "y": 60}},
            "abuse":{"no":{"x": 190, "y": 60},"yes":{"x": 190, "y": 60}},
            "psycho":{"neg":{"x": 190, "y": 60},"pos":{"x": 190, "y": 60}},
            "triage":{"emr":{"x": 190, "y": 60},"urg":{"x": 190, "y": 60},"nur":{"x": 190, "y": 60}},
            "assessor":{"x": 190, "y": 60}

          }
        }
      },
      {
        "pageno":2,
        "img":"Admission Note (Gen).xps.2.png",
        "position":{
          "dtemplate":{"style":"position:relative;left:12cm; top:-29.5cm; z-index:1; width: 433px; height: 144px;"},
          "dem": {
            "name": {"x": 100, "y": 50},

            "hn": {"x": 100, "y": 55},
            "dob": {"x": 150, "y": 55},
            "age": {"x": 180, "y": 55},

            "dept": {"x": 100, "y": 60},
            "date": {"x": 150, "y": 60},
            "time": {"x": 190, "y": 60},

            "phys": {"x": 100, "y": 65},
            "alg": {"nka": {"x": 100, "y": 55}, "text": {"x": 100, "y": 55}},
            "food": {"nka": {"x": 100, "y": 55}, "text": {"x": 100, "y": 55}},
            "other": {"x": 190, "y": 60}
          },
          "scrn":{
            "status":{"walk":{"x": 190, "y": 60},"wheel":{"x": 190, "y": 60},"stretch":{"x": 190, "y": 60}},
            "bw":{"x": 190, "y": 60},
            "ht":{"x": 190, "y": 60},
            "bmi":{"x": 190, "y": 60},
            "ccn":{"x": 190, "y": 60},
            "obs":{"bt":{"x": 190, "y": 60},"sbp":{"x": 190, "y": 60},"dbp":{"x": 190, "y": 60},"pr":{"x": 190, "y": 60},"rr":{"x": 190, "y": 60},"sat":{"x": 190, "y": 60}},
            "pain":{
              "score":{"x": 190, "y": 60},
              "loc":{"x": 190, "y": 60},
              "assess":{"nrs":{"x": 190, "y": 60},"frs":{"x": 190, "y": 60}},"flacc":{"x": 190, "y": 60},"nps":{"x": 190, "y": 60},"bps":{"x": 190, "y": 60},"pps":{"x": 190, "y": 60}},
            "fall":{"no":{"x": 190, "y": 60},"yes":{"x": 190, "y": 60},"area":{"x": 190, "y": 60}},
            "abuse":{"no":{"x": 190, "y": 60},"yes":{"x": 190, "y": 60}},
            "psycho":{"neg":{"x": 190, "y": 60},"pos":{"x": 190, "y": 60}},
            "triage":{"emr":{"x": 190, "y": 60},"urg":{"x": 190, "y": 60},"nur":{"x": 190, "y": 60}},
            "assessor":{"x": 190, "y": 60}

          }
        }
      },
      {
        "pageno":3,
        "img":"Admission Note (Gen).xps.3.png",
        "position":{
          "dtemplate":{"style":"position:relative;left:12.1cm; top:-29.2cm; z-index:1; width: 433px; height: 144px;"},
          "dem": {
            "name": {"x": 100, "y": 50},

            "hn": {"x": 100, "y": 55},
            "dob": {"x": 150, "y": 55},
            "age": {"x": 180, "y": 55},

            "dept": {"x": 100, "y": 60},
            "date": {"x": 150, "y": 60},
            "time": {"x": 190, "y": 60},

            "phys": {"x": 100, "y": 65},
            "alg": {"nka": {"x": 100, "y": 55}, "text": {"x": 100, "y": 55}},
            "food": {"nka": {"x": 100, "y": 55}, "text": {"x": 100, "y": 55}},
            "other": {"x": 190, "y": 60}
          },
          "scrn":{
            "status":{"walk":{"x": 190, "y": 60},"wheel":{"x": 190, "y": 60},"stretch":{"x": 190, "y": 60}},
            "bw":{"x": 190, "y": 60},
            "ht":{"x": 190, "y": 60},
            "bmi":{"x": 190, "y": 60},
            "ccn":{"x": 190, "y": 60},
            "obs":{"bt":{"x": 190, "y": 60},"sbp":{"x": 190, "y": 60},"dbp":{"x": 190, "y": 60},"pr":{"x": 190, "y": 60},"rr":{"x": 190, "y": 60},"sat":{"x": 190, "y": 60}},
            "pain":{
              "score":{"x": 190, "y": 60},
              "loc":{"x": 190, "y": 60},
              "assess":{"nrs":{"x": 190, "y": 60},"frs":{"x": 190, "y": 60}},"flacc":{"x": 190, "y": 60},"nps":{"x": 190, "y": 60},"bps":{"x": 190, "y": 60},"pps":{"x": 190, "y": 60}},
            "fall":{"no":{"x": 190, "y": 60},"yes":{"x": 190, "y": 60},"area":{"x": 190, "y": 60}},
            "abuse":{"no":{"x": 190, "y": 60},"yes":{"x": 190, "y": 60}},
            "psycho":{"neg":{"x": 190, "y": 60},"pos":{"x": 190, "y": 60}},
            "triage":{"emr":{"x": 190, "y": 60},"urg":{"x": 190, "y": 60},"nur":{"x": 190, "y": 60}},
            "assessor":{"x": 190, "y": 60}

          }
        }
      }
    ]
  }
];


function headData(){
  return hdata;

}

var hdata=
  [
    {
      "tpltNo":1,
      "template":"<table><tr><td colspan='3' style='padding-left:1cm;' id='dName'>name</td></tr><tr><td colspan='3' style='padding-left:1cm;' id='dHN'>{{hn}}</td></tr><tr><td style='padding-left:1.7cm;width:1.9cm;' id='dDOB'>{{dob}}</td><td style='padding-left:0.3cm;width:1.5cm;' id='dAge'>{{age}}</td><td style='padding-left:0.2cm;' id='dSex'>{{sex}}</td></tr><tr><td style='padding-left:1cm;' id='dWard'>{{ward}}</td><td style='padding-left:0.7cm;' id='dRoom'>{{room}}</td><td>&nbsp;</td></tr><tr><td colspan='3' style='padding-left:2.5cm;' id='dAttn'>{{attndr}}</td></tr></table>"
    }
  ];
