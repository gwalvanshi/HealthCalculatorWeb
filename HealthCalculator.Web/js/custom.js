


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

