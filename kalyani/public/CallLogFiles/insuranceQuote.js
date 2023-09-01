$(document).ready(function () {
    var typeOfpage = $("#typeOfDispoPageView").val();
    //console.log(typeOfpage);

    if (typeOfpage === "insurance" || typeOfpage == "insuranceSearch") {
        var urlDisposition = siteRoot+"/InsuranceOutBound/insuranceQuote/";
        $.ajax({
            type: 'POST',
            url: urlDisposition,
            datatype: 'json',
            data: {},
            cache: false,
            success: function (res) {
                if (undefined !== res.vehTypZone) {

                    if (res.vehTypZone.length > 1) {
                        var dropdown = document.getElementById("zoneId");

                        for (var i = 0; i < res.vehTypZone.length; i++) {

                            dropdown[dropdown.length] = new Option(res.vehTypZone[i], res.vehTypZone[i]);

                        }
                    
                    } 


                    }
                
            }, error(error) {

            }
        });
    }

    var urlInsuranceCompanyList = siteRoot+"/InsuranceOutBound/insuranceCompanyList/"
    $.ajax({
        type: 'POST',
        url: urlInsuranceCompanyList,
        datatype: 'json',
        data: {},
        cache: false,
        success: function (res) {
            if (undefined !== res.insData) {

                $('#insuranceCompanyId').empty();
                var dropdown = document.getElementById("insuranceCompanyId");
                dropdown[0] = new Option('select', '0');
                for (var i = 0; i < res.insData.length; i++) {

                    dropdown[dropdown.length] = new Option(res.insData[i], res.insData[i]);

                }

            }
        }, error(error) {

        }
    });
});

function resetFinalPremium() {
    $('.resetAll').val('0');
    $('#idvId').val('0.0');
    $('#commentIQId').val("");
    $('#finalPremiumId').val("");
    $('#zoneId').prop('selectedIndex', 0);
    $('#typeId').prop('selectedIndex', 0);
    $('#cubicCapacityId').prop('selectedIndex', 0);
    $('#ageId').prop('selectedIndex', 0);
    $('#insuranceCompanyId').prop('selectedIndex', 0);
}

function getVehTypeBasedOnZoneFilter() {

    var selectedZone = document.getElementById('zoneId').value;
    var urlDisposition = siteRoot+"/InsuranceOutBound/vehicleTypeByZoneData/";

    $.ajax({
        type: 'POST',
        url: urlDisposition,
        datatype: 'json',
        data: { selectedZone: selectedZone},
        cache: false,
        success: function (res) {
            if (undefined !== res.vehTypeData) {

                $('#typeId').empty();
                var dropdown = document.getElementById("typeId");
                dropdown[0] = new Option('select', '0');
                for (var i = 0; i < res.vehTypeData.length; i++) {
                    dropdown[dropdown.length] = new Option(res.vehTypeData[i], res.vehTypeData[i]);
                }

            }
        }, error(error) {

        }
    });
}

function getCCBasedOnVehTypeFilter() {

    var selectedType = document.getElementById('typeId').value;
    var urlDisposition = siteRoot+"/InsuranceOutBound/ccByVehTypeData/" ;

    $.ajax({
        type: 'POST',
        url: urlDisposition,
        datatype: 'json',
        data: { selectedType: selectedType},
        cache: false,
        success: function (res) {
            if (undefined !== res.ccData) {

                $('#cubicCapacityId').empty();
                var dropdown = document.getElementById("cubicCapacityId");
                dropdown[0] = new Option('select', '0');
                for (var i = 0; i < res.ccData.length; i++) {

                    dropdown[dropdown.length] = new Option(res.ccData[i], res.ccData[i]);

                }

            }
        }, error(error) {

        }
    });
}

function getAgeBasedOnCCFilter() {

    var selectedcc = document.getElementById('cubicCapacityId').value;
    var urlDisposition = siteRoot+"/InsuranceOutBound/ageByCCTypeData/";

    $.ajax({
        type: 'POST',
        url: urlDisposition,
        datatype: 'json',
        data: { selectedcc: selectedcc},
        cache: false,
        success: function (res) {
            if (undefined !== res.ageData) {

                $('#ageId').empty();
                var dropdown = document.getElementById("ageId");
                dropdown[0] = new Option('select', '0');
                for (var i = 0; i < res.ageData.length; i++) {

                    dropdown[dropdown.length] = new Option(res.ageData[i], res.ageData[i]);

                }

            }
        }, error(error) {

        }
    });
}


function getIRBasedOnFilter() {

    var selectedZone = document.getElementById('zoneId').value;
    var selectedType = document.getElementById('typeId').value;
    var selectedcc = document.getElementById('cubicCapacityId').value;
    var selectedAge = document.getElementById('ageId').value;

    var urlDisposition = siteRoot+"/InsuranceOutBound/IRBasedOnFilter/";

    $.ajax({
        type: 'POST',
        url: urlDisposition,
        datatype: 'json',
        data: { selectedZone: selectedZone, selectedType: selectedType, selectedcc: selectedcc, selectedAge: selectedAge},
        cache: false,
        success: function (res) {
            if (res.irData != " ") {

                

                $('#odId').html(res.irData[0].odPercentage);
                $('#odId').val(res.irData[0].odPercentage);

                $('#thirdPartyPremId').html(res.irData[0].thirdPartyPremium);
                $('#thirdPartyPremId').val(res.irData[0].thirdPartyPremium);

                $('#paPremiumId').html(res.irData[0].paLL);
                $('#paPremiumId').val(res.irData[0].paLL);

                basicODCalFunction();
            }
        }, error(error) {

        }
    });
}



$('#idvId').on("keyup", function () {

    basicODCalFunction();

});

$('#odId').on("keyup", function () {

    this.value = this.value
        .replace(/[^\d.]/g, '')             // numbers and decimals only
        .replace(/(^[\d]{1})[\d]/g, '$1')   // not more than 1 digits at the beginning
        .replace(/(\..*)\./g, '$1')         // decimal can't exist more than once
        .replace(/(\.[\d]{3})./g, '$1');


    basicODCalFunction();

});

function basicODCalFunction() {

    var idvValue = document.getElementById('idvId').value;
    var odvValue = document.getElementById('odId').value;

    console.log(" idvValue " + idvValue + " odvValue : " + odvValue);

    if (idvValue == "") {

        idvValue = 0;

    }
    if (odvValue == "") {

        odvValue = 0;

    }

    var urlLink = siteRoot+"/InsuranceOutBound/getBasicODVaue/";

    $.ajax({
        type: 'POST',
        url: urlLink,
        datatype: 'json',
        data: { odvValue: odvValue, idvValue: idvValue},
        cache: false,
        success: function (basicodValue) {
            $('#basicODId').html(basicodValue.val);
            $('#basicODId').val(basicodValue.val);
            getDiscountPercentageValue();
        }, error(error) {

        }
    });
}


$('#commercialDiscId').on("keyup", function () {

	/*var val = this.value;	
	var re1 = /^([0-9]+[\.]?[0-9]?[0-9]?|[0-9])/g;
        val = re1.exec(val);
        console.log(val);
        if (val) {
            this.value = val[0];
        } else {
            this.value = "";
        }*/

    this.value = this.value
        .replace(/[^\d.]/g, '')             // numbers and decimals only
        .replace(/(^[\d]{2})[\d]/g, '$1')   // not more than 2 digits at the beginning
        .replace(/(\..*)\./g, '$1')         // decimal can't exist more than once
        .replace(/(\.[\d]{2})./g, '$1');

    getDiscountPercentageValue();

});

function getDiscountPercentageValue() {
    console.log("disc");
    var od1Value = document.getElementById('basicODId').value;
    var discPerc = document.getElementById('commercialDiscId').value;

    var discVal = Math.round(od1Value * discPerc) / 100;

    $('#discValueId').html(discVal);
    $('#discValueId').val(discVal);

    var od2Is = od1Value - discVal;


    $('#totalODPremiumId').html(Math.round(od2Is));
    $('#totalODPremiumId').val(Math.round(od2Is));
    get0d3Total();



}
$('#cngId').on("keyup", function () {

    get0d3Total();

});
$('#elecId').on("keyup", function () {

    get0d3Total();

});
function get0d3Total() {
    console.log("0d3");
    var cngValue = document.getElementById('cngId').value;
    var elecValue = document.getElementById('elecId').value;
    var OD2Value = document.getElementById('totalODPremiumId').value;
    var od3Is = (+cngValue) + (+elecValue) + (+OD2Value);

    console.log("cngValue" + cngValue);
    console.log("elecValue" + elecValue);
    console.log("od3Is" + od3Is);
    $('#od3Id').html(od3Is);
    $('#od3Id').val(od3Is);
    get0d4Total();
}

$('#antitheftId').on("keyup", function () {

    get0d4Total();

});

function get0d4Total() {
    console.log("od4");
    var od3Value = document.getElementById('od3Id').value;
    var antiValue = document.getElementById('antitheftId').value;

    var od4Is = od3Value - antiValue;


    $('#od4Id').html(od4Is);
    $('#od4Id').val(od4Is);

    getNCBPerc();
}

$('#ncbPercenId').on("keyup", function () {

    this.value = this.value
        .replace(/[^\d.]/g, '')             // numbers and decimals only
        .replace(/(^[\d]{2})[\d]/g, '$1')   // not more than 2 digits at the beginning
        .replace(/(\..*)\./g, '$1')         // decimal can't exist more than once
        .replace(/(\.[\d]{2})./g, '$1');


    getNCBPerc();

});

function getNCBPerc() {
    console.log("ncb");
    var od4Value = document.getElementById('od4Id').value;
    var ncbPerc = document.getElementById('ncbPercenId').value;

    var ncbVal = Math.round(od4Value * ncbPerc) / 100;



    $('#ncbValueId').html(ncbVal);
    $('#ncbValueId').val(ncbVal);

    var od5Is = od4Value - ncbVal;


    $('#od5Id').html(od5Is);
    $('#od5Id').val(od5Is);
    getFinalOdPremium();
}

$('#imt23Id').on("keyup", function () {

    getFinalOdPremium();

});

function getFinalOdPremium() {
    console.log("final");
    var od5Value = document.getElementById('od5Id').value;
    var imt23IdVal = document.getElementById('imt23Id').value;

    var finalodIs = (+od5Value) + (+imt23IdVal);
    $('#odPremiumId').html(Math.round(finalodIs));
    $('#odPremiumId').val(Math.round(finalodIs));

    getFinalADDON();
}

//ADD ON


$('#zpId').on("keyup", function () {

    getFinalADDON();

});
$('#rtiId').on("keyup", function () {

    getFinalADDON();

});
$('#epId').on("keyup", function () {

    getFinalADDON();

});
$('#addon4Id').on("keyup", function () {

    getFinalADDON();

});

function getFinalADDON() {
    console.log("addon");
    var zpValue = document.getElementById('zpId').value;
    var rtiVal = document.getElementById('rtiId').value;
    var epVal = document.getElementById('epId').value;
    var addonVal = document.getElementById('addon4Id').value;

    var finaladdOnis = (+zpValue) + (+rtiVal) + (+epVal) + (+addonVal);

    $('#netaddonId').html(Math.round(finaladdOnis));
    $('#netaddonId').val(Math.round(finaladdOnis));
    getFinalLiability();


}

//LL Calculation
$('#thirdPartyPremId').on("keyup", function () {

    getFinalLiability();

});
$('#paPremiumId').on("keyup", function () {

    getFinalLiability();

});
$('#lp3Id').on("keyup", function () {

    getFinalLiability();

});
$('#lp4Id').on("keyup", function () {

    getFinalLiability();

});

function getFinalLiability() {

    console.log("ll");

    var tppValue = document.getElementById('thirdPartyPremId').value;
    var paPVal = document.getElementById('paPremiumId').value;
    var lp3Val = document.getElementById('lp3Id').value;
    var lp4Val = document.getElementById('lp4Id').value;

    var finalliablilityis = (+tppValue) + (+paPVal) + (+lp3Val) + (+lp4Val);

    $('#totalPremiumID').html(Math.round(finalliablilityis));
    $('#totalPremiumID').val(Math.round(finalliablilityis));
    getTotalPkgBFTax();

}


//totalPackage

$('#otherAMTID').on("keyup", function () {

    getTotalPkgBFTax();

});

function getTotalPkgBFTax() {

    console.log("tt Bf");

    var otherValue = document.getElementById('otherAMTID').value;
    var netAddOnVal = document.getElementById('netaddonId').value;
    var netLPVal = document.getElementById('totalPremiumID').value;
    var finalOdVal = document.getElementById('odPremiumId').value;

    var totalPKG = (+otherValue) + (+netAddOnVal) + (+netLPVal) + (+finalOdVal);

    $('#totalPackagePremiumId').html(Math.round(totalPKG));
    $('#totalPackagePremiumId').val(Math.round(totalPKG));

    getGrossPkgAFTax();
}


$('#serviceTax').on("keyup", function () {

    this.value = this.value
        .replace(/[^\d.]/g, '')             // numbers and decimals only
        .replace(/(^[\d]{2})[\d]/g, '$1')   // not more than 2 digits at the beginning
        .replace(/(\..*)\./g, '$1')         // decimal can't exist more than once
        .replace(/(\.[\d]{2})./g, '$1');


    getGrossPkgAFTax();

});

function getGrossPkgAFTax() {

    console.log("GAFT");

    var srvValue = document.getElementById('serviceTax').value;
    var totalPkgVal = document.getElementById('totalPackagePremiumId').value;

    var grossVal = Math.round(srvValue * totalPkgVal) / 100;

    var actualValue = Math.round((+grossVal) + (+totalPkgVal));

    $('#finalPremiumId').html(actualValue);
    $('#finalPremiumId').val(actualValue);
}


$('#saveFinalPremium').click(function () {

    //var email = $("#ddl_emails").val();
    //if (email == "" || email == null) {

    //    Lobibox.notify('warning', {
    //        continuedelayoninactivetab: true,
    //        msg: 'email should not be null'
    //    });
    //    return false;
    //}

    var cngValue = document.getElementById('cngId').value;
    var elecValue = document.getElementById('elecId').value;
    var od3Value = document.getElementById('od3Id').value;
    var antiValue = document.getElementById('antitheftId').value;
    var od4Value = document.getElementById('od4Id').value;
    var od5Value = document.getElementById('od5Id').value;
    var imt23IdVal = document.getElementById('imt23Id').value;
    var zpValue = document.getElementById('zpId').value;
    var rtiVal = document.getElementById('rtiId').value;
    var epVal = document.getElementById('epId').value;
    var addonVal = document.getElementById('addon4Id').value;
    var lp3Val = document.getElementById('lp3Id').value;
    var lp4Val = document.getElementById('lp4Id').value;
    var otherValue = document.getElementById('otherAMTID').value;



    var idv = document.getElementById('idvId').value;
    var odPercentage = document.getElementById('odId').value;
    var odRupees = document.getElementById('basicODId').value;
    var ncbPercentage = document.getElementById('ncbPercenId').value;
    var ncbRupees = document.getElementById('ncbValueId').value;
    var oDPremium = document.getElementById('odPremiumId').value;
    var discountPercentage = document.getElementById('commercialDiscId').value;
    var discountValue = document.getElementById('discValueId').value;
    var totalODPremium = document.getElementById('totalODPremiumId').value;
    var insuranceCompany = document.getElementById('insuranceCompanyId').value;
    var thirdPartyPremium = document.getElementById('thirdPartyPremId').value;
    var paPremium = document.getElementById('paPremiumId').value;
    var liabilityPremium = document.getElementById('totalPremiumID').value;
    var addOn = document.getElementById('netaddonId').value;
    var totalPremiumWithOutTax = document.getElementById('totalPackagePremiumId').value;
    var serviceTax = document.getElementById('serviceTax').value;
    var totalPremiumWithTax = document.getElementById('finalPremiumId').value;
    var commentIQ = document.getElementById('commentIQId').value;
    var customer_id = document.getElementById('customer_Id').value;
    var vehical_Id = document.getElementById('vehical_Id').value;


    var insuranceQuotaInfo = {
        "idv": idv,
        "odPercentage": odPercentage,
        "odRupees": odRupees,
        "ncbPercentage": ncbPercentage,
        "ncbRupees": ncbRupees,
        "oDPremium": oDPremium,
        "discountPercentage": discountPercentage,
        "discountValue": discountValue,
        "totalODPremium": totalODPremium,
        "thirdPartyPremium": thirdPartyPremium,
        "paPremium": paPremium,
        "liabilityPremium": liabilityPremium,
        "addOn": addOn,
        "totalPremiumWithOutTax": totalPremiumWithOutTax,
        "serviceTax": serviceTax,
        "totalPremiumWithTax": totalPremiumWithTax,
        "commentIQ": commentIQ,
        "customer_id": customer_id,
        "vehicle_id": vehical_Id,
        "insuranceCompanyQuotated": insuranceCompany,
        "cng": cngValue,
        "elec": elecValue,
        "od3": od3Value,
        "antitheft": antiValue,
        "od4": od4Value,
        "od5": od5Value,
        "imt23": imt23IdVal,
        "zp": zpValue,
        "rti": rtiVal,
        "ep": epVal,
        "addon4": addonVal,
        "lp3": lp3Val,
        "lp4": lp4Val,
        "otherAmt": otherValue

    };
    var insuranceQuota_Info = [];
    insuranceQuota_Info.push(insuranceQuotaInfo);
    jsonData = JSON.stringify(insuranceQuotaInfo);
    var urlPath = siteRoot+"/InsuranceOutBound/saveInsuranceQuotation";


    $.ajax({
        type: 'POST',
        url: urlPath,
        datatype: 'json',
        data: { jsonData: jsonData},
        cache: false,
        success: function (res) {
            if (res.success == true) {
                alert('Quote added successfully');
            }
        }, error(error) {

        }
    });
});