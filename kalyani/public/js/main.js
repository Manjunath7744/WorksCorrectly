/*
 * jQuery File Upload Plugin JS Example
 * https://github.com/blueimp/jQuery-File-Upload
 *
 * Copyright 2010, Sebastian Tschan
 * https://blueimp.net
 *
 * Licensed under the MIT license:
 * https://opensource.org/licenses/MIT
 */
var siteRoot = '@Request.ApplicationPath';
if (siteRoot === '/') {
    siteRoot = "";
}
/* global $, window */

$(function () {
    'use strict';

    // Initialize the jQuery File Upload widget:
if($('#fileupload').length){
	
	
    $('#fileupload').fileupload({
        // Uncomment the following to send cross-domain cookies:
        //xhrFields: {withCredentials: true},
        url: siteRoot +'FileUpload/startUpload'
    });

    // Enable iframe cross-domain access via redirect option:
    $('#fileupload').fileupload(
        'option',
        'redirect',
        window.location.href.replace(
            /\/[^\/]*$/,
            '/cors/result.html?%s'
        )
    );
	
	//Get the selected option to send an ajax call to get the existing list of files
	var selectedType = $('#uploadType :selected').text();
	//alert(selectedType);
	$('#uploadTypeHidden').val(selectedType)
	//First time get all the files uploaded for the selected upload type
    $.ajax({
        url: siteRoot +'/FileUpload/getExistingFiles/',
        dataType: 'json',
        data: { upType: selectedType },
    context: $('#fileupload')[0]
	}).done(function (result) {
		$(this).fileupload('option', 'done').call(this, null, {result: result});
		});

	// On selection change, display the uploaded types for the selected option
$('#uploadType').on('change' ,function(e){
	$("table tbody.files").empty();
	var selectedType = $('#uploadType :selected').text();
	$('#uploadTypeHidden').val(selectedType)
			$.ajax({
                url: siteRoot +'/FileUpload/getExistingFiles/',
                dataType: 'json',
                data: { upType: selectedType },

    context: $('#fileupload')[0]
            }).done(function (result) {
                console.log(result);        
		$(this).fileupload('option', 'done').call(this, null, {result: result});
		});

});
		
		
       // Demo settings:
    $('#fileupload').fileupload('option', {
        url: siteRoot +'/FileUpload/startUpload/',
        // Enable image resizing, except for Android and Opera,
        // which actually support image resizing, but fail to
        // send Blob objects via XHR requests:
        disableImageResize: /Android(?!.*Chrome)|Opera/
            .test(window.navigator.userAgent),
        maxFileSize: 5999000,
        acceptFileTypes: /(\.|\/)(xlsx|xls)$/i
    });

    $('#fileupload').fileupload({
        url: siteRoot +'/FileUpload/startUpload',
        error: function (e, data, txt) {
            console.log(txt);
        }
    });




        // Upload server status check for browsers with CORS support:
        //if ($.support.cors) {
        //    $.ajax({
        //        url: '/Upload/postfile/',
        //        type: 'HEAD'
        //    }).fail(function () {
        //        $('<div class="alert alert-danger"/>')
        //            .text('Upload server currently unavailable - ' +
        //                    new Date())
        //            .appendTo('#fileupload');
                
        //    });
        //}
  
}
});
