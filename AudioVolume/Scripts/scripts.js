AOS.init({
  once: true,
});

function homeJs() {
  $(document).ready(function () {
    fix_pos_menu_home();
  });

  $(".header-nav-new .header-nav-main .header-nav-main-content").addClass(
    "active"
  );
  $(".main-menu-list li").on("mouseenter", function () {
    var height_nav = $(".header-nav-main-content > ul.main-menu-list").height();
    $(".main-menu-content").css({
      height: height_nav,
    });
  });

  $("div.main-menu-content > ul > li > ul > p").on("click", function () {
    $(this).hide();
    $(this).parent().find("li").show();
  });

  var product_slider = $("#product_slider_catalog");
  product_slider.owlCarousel({
    nav: false,
    loop: true,
    autoplay: true,
    autoplayTimeout: 3000,
    autoplayHoverPause: true,
    margin: 10,
    responsive: {
      0: {
        items: 2,
      },
      600: {
        items: 2,
      },
      1000: {
        items: 5,
      },
    },
  });
  $("#nav_product_slider_catalog_left").click(function () {
    product_slider.trigger("next.owl.carousel");
  });
  $("#nav_product_slider_catalog_right").click(function () {
    product_slider.trigger("prev.owl.carousel", [300]);
  });

  $(window).resize(function () {
    fix_pos_menu_home();
  });
  function fix_pos_menu_home() {
    $(".home-menu li").each(function () {
      var h_a = $(this).find("a").height();
      $(this)
        .find("a")
        .css("margin-top", -h_a / 2);
    });
  }
}
//$('#footer_images').owlCarousel({
//    items: 3,
//    lazyLoad: true,
//    loop: true,
//    margin: 10,
//    nav: false,
//    dots: false,
//    autoplay: true,
//    autoplayTimeout: 4000,
//    autoplayHoverPause: true
//});
$("div.dropdown-shop-small-cnt").hide();
$("div.dropdown-shop-small").click(function () {
  $("div.dropdown-shop-small-cnt").toggle(500);
});
$(".header-mobile-right .header_icon_cart").on("click", function () {
  $(".header-mobile-right").find(".dropdown-shop-small-cnt").toggle(100);
});

function productDetail() {
  $(".slider-for").slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    fade: true,
    asNavFor: ".slider-nav",
    prevArrow:
      '<button class="chevron-prev"><i class="fas fa-chevron-left"></i></button>',
    nextArrow:
      '<button class="chevron-next"><i class="fas fa-chevron-right"></i></button>',
  });

  $(".slider-nav").slick({
    slidesToShow: 4,
    slidesToScroll: 1,
    asNavFor: ".slider-for",
    speed: 1000,
    dots: false,
    focusOnSelect: true,
    prevArrow:
      '<button class="chevron-prev"><i class="fas fa-chevron-left"></i></button>',
    nextArrow:
      '<button class="chevron-next"><i class="fas fa-chevron-right"></i></button>',
    responsive: [
      {
        breakpoint: 480,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 1,
          vertical: false,
        },
      },
    ],
  });

  $(".feedback-product").slick({
    dots: false,
    infinite: true,
    slidesToShow: 6,
    slidesToScroll: 1,
    speed: 1000,
    autoplay: false,
    autoplaySpeed: 3000,
    prevArrow:
      '<button class="chevron-prev"><i class="far fa-chevron-left"></i></button>',
    nextArrow:
      '<button class="chevron-next"><i class="far fa-chevron-right"></i></button>',
    responsive: [
      {
        breakpoint: 992,
        settings: {
          slidesToShow: 2,
        },
      },
      {
        breakpoint: 600,
        settings: {
          slidesToShow: 1,
        },
      },
    ],
  });
}

$("#keysearch").on('click', function () {
    $('.review-product-search').css('display', 'block')
})
$(".close-suggest-search").on('click', function () {
    $('.review-product-search').css('display', 'none')
})
function openBar() {
    $(".menu-mb").toggleClass("active");
    $(".overlay").toggleClass("active");
}
$(".overlay").click(function () {
    $(this).removeClass("active");
    $(".menu-mb").removeClass("active");
});

$(document).ready(function () {
    $('#keysearch').on('keyup', function () {
        var keysearch = $(this).val().trim();
        if (keysearch.length >= 3) {
            $.ajax({
                url: 'Home/SearchProduct',
                type: 'POST',
                data: { keysearch: keysearch },
                success: function (result) {
                    $('#review-product-search-content').html(result);
                    $('#review-product-search').show();
                },
                error: function () {
                    $('#review-product-search-content').html('<p>Đã xảy ra lỗi trong quá trình tìm kiếm.</p>');
                    $('#review-product-search').show();
                }
            });
        } else {
            $('#review-product-search').hide();
        }
    });

    $('#close-suggest-search').on('click', function () {
        $('#review-product-search').hide();
    });
});
