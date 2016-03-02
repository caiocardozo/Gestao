
      $(function(){
          var div = $('#menuHeader');
          $(window).scroll(function () {
              if ($(this).scrollTop() > 50) {
                  div.addClass("menu-fixo");
              } else {
                  div.removeClass("menu-fixo");
              }
          });
      });
