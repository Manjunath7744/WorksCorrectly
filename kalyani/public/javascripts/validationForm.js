$(document).ready(function() {
    $("#myFormValidation").formValidation({
    	 message: 'This value is not valid',
        icon: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
        	dealerId: {
                
                validators: {
                    notEmpty: {
                        message: 'The dealerId  is required and cannot be empty'
                    }
                }
            },
            dealerName: {
               
                validators: {
                    notEmpty: {
                        message: 'The dealerName  is required and cannot be empty'
                    }
                }
            },
            dealerAddress: {
               
                validators: {
                    notEmpty: {
                        message: 'The dealerAddress  is required and cannot be empty'
                    }
                }
            },
            dealerCode: {
                message: 'The username is not valid',
                validators: {
                    notEmpty: {
                        message: 'The dealerCode is required and cannot be empty'
                    },
                    stringLength: {
                        min: 5,
                        max: 30,
                        message: 'The dealerCode must be more than 5 and less than 30 characters long'
                    }
                }
            }
        }
    });
});