
$(document).ready(function() { 
$('#aright').tabSlideOut({
      tabLocation: 'right',
      offsetReverse: true,
	    offset: '500px',
      handleOffsetReverse: true
    });
    $('#right').tabSlideOut({
      tabLocation: 'right',
      offsetReverse: true,
	   offset: '300px',
      handleOffsetReverse: true
    });
	
     $('#bright').tabSlideOut({
      tabLocation: 'right',
      offsetReverse: true,
	   offset: '100px',
      handleOffsetReverse: true
    });
    
   $('#bottom').click(function() {
    $(this).next().animate({width: 'toggle'});
  });
    
});



