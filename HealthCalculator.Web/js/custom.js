

$(document).ready(function() {
	/* Homepage thumbnail slider */
	$("#content-slider").lightSlider({
		loop:true,
		keyPress:true,
		auto:true,
		pauseOnHover: true,
		slideMove:2,
		item:3,
		easing: 'cubic-bezier(0.25, 0, 0.25, 1)',
		responsive : [
            {
                breakpoint:8000,
                settings: {
                    item:1,
                    slideMove:1,
                    slideMargin:6,
                  }
            },
            {
                breakpoint:4800,
                settings: {
                    item:1,
                    slideMove:1
                  }
            }
        ]

	});
	
	/* Animation Effects */
	$(".effects").smoove({offset:"30%"});
});



/* ***************** show / hide div ******************************** */
$('.nextStep a').on('click', function ()
{
    if (isValidChild) {
        var target = $(this).attr('rel');
        $("." + target).show().siblings("div").hide();
    }
});
/* ***************** show / hide div end ******************************** */

/* ***************** show / hide div ******************************** */
$('.nextStepA a').on('click', function ()
{
    if (isValidAdult) {
        var target = $(this).attr('rel');
        $("." + target).show().siblings("div").hide();
    }
});
/* ***************** show / hide div end ******************************** */

