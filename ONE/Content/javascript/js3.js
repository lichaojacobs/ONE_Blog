/* @script.js文件 */
$(document).ready(function () {
	// Layout
	if ($('body').hasClass('p-homepage')) {
		// ordering posts
		var $container = $('.m-postlst');
		$container.imagesLoaded(function(){
			$container.masonry();
			// Fixing layout
			//fixLayout();
		});
    } else if ($('body').hasClass('p-detailpage')) {
		if ($('.m-cmt').length <= 0 && $('.m-hot').length <= 0) {   // cmthot blk
			$('.m-cmthot').hide();
		}	
		if ($.browser.msie && $.browser.version === '10.0') {  // <ul>,<ol>
			$('html').addClass('ie10');
		}
	}
	// search
	bindSchEvents($('#j-lnksch'), $('#j-schform .txt'));
});

function bindSchEvents($lnk, $txt){
	$lnk.bind('click', function () {
		$(this).addClass('f-hidden').parent().addClass('m-schshow');
		setTimeout(function () {
			$txt.focus();
		}, 300);
		return false;
	});
	$txt.bind('blur', function () {
		//setTimeout(function () {
			$lnk.removeClass('f-hidden').parent().removeClass('m-schshow');
		//}, 300);
		return false;
	});
}