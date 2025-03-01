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

$(document).ready(function () {
  var review_pro_search = document.getElementById("review-product-search");
  var review_product_search_content = document.getElementById(
    "review-product-search-content"
  );
  var review_pro_search_mobile = document.getElementById(
    "review-product-search-mobile"
  );
  var review_product_search_content_mobile = document.getElementById(
    "review-product-search-content-mobile"
  );

  var close_suggest_search = document.getElementById("close-suggest-search");
  var close_suggest_search_mobile = document.getElementById(
    "close-suggest-search-mobile"
  );

  var keysearch_header = document.getElementById("keysearch");
  var keysearch_header_mobile = document.getElementById("keysearch-mobile");

  keysearch_header.onblur = keysearch_header.onfocus = function () {
    review_pro_search.style.display = "block";
  };

  keysearch_header_mobile.onblur = keysearch_header_mobile.onfocus =
    function () {
      review_pro_search_mobile.style.display = "block";
    };

  //bg_black_full_page.onclick = close_suggest_search.onclick = close_suggest_search_mobile.onclick = close_all_suggest
  function close_all_suggest() {
    review_pro_search.style.display = "none";
    review_pro_search_mobile.style.display = "none";
  }

  keysearch_header.onkeyup = function () {
    var keysearch = encodeURIComponent(keysearch_header.value);
    if (keysearch.length >= 3) {
      $.ajax({
        url: "Home/SearchProduct",
        method: "POST",
        dataType: "json",
        data: "keysearch=" + keysearch,
      }).done(function (data) {
        console.log(data);
        var x =
          data.data +
          '<div class="close-suggest-search" id="close-suggest-search">Đóng</div>';
        review_product_search_content.innerHTML = x;
        document.getElementById("close-suggest-search").onclick =
          close_all_suggest;
      });
    }
    };
    $(".expand-bar").click(function () {
        $(this).toggleClass('open');
        $(this).siblings('.sub-nav-mb').slideToggle();
    });
    


  keysearch_header_mobile.onkeyup = function () {
    var keysearch = encodeURIComponent(keysearch_header_mobile.value);
    if (keysearch.length >= 3) {
      $.ajax({
        url: "Home/SearchProduct",
        method: "POST",
        dataType: "json",
        data: "keysearch=" + keysearch,
      }).done(function (data) {
        var x =
          data.data +
          '<div class="close-suggest-search" id="close-suggest-search-mobile">Đóng</div>';
        review_product_search_content_mobile.innerHTML = x;
        document.getElementById("close-suggest-search-mobile").onclick =
          close_all_suggest;
      });
    }
  };
});

function openBar() {
    $(".menu-mb").toggleClass("active");
    $(".overlay").toggleClass("active");
}
$(".overlay").click(function () {
    $(this).removeClass("active");
    $(".menu-mb").removeClass("active");
});