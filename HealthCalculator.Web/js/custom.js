


/* ***************** show / hide div ******************************** */
$('.nextStep a').on('click', function() {
	var target = $(this).attr('rel');
	$("." + target).show().siblings("div").hide();
});
/* ***************** show / hide div end ******************************** */

