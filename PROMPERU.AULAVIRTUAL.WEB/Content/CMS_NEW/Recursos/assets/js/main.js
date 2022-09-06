$(document).ready(function() {
	var scrollBottom = $(window).scrollTop() + $(window).height();
    $(window).scroll(function(){
		if($(this).scrollTop() > 5) {
			$('header').addClass('fixed');
		} else {
			$('header').removeClass('fixed');
		}
    });
    // Tablas check
    $(".checkAll").on("click", function() {
    $(this)
      .closest("table")
      .find("tbody :checkbox")
      .prop("checked", this.checked)
      .closest("tr")
      .toggleClass("selected", this.checked);
  });

  $("tbody :checkbox").on("click", function() {
    // toggle selected class to the checkbox in a row
    $(this)
      .closest("tr")
      .toggleClass("selected", this.checked);

    // add selected class on check all
    $(this).closest("table")
      .find(".checkAll")
      .prop("checked",
        $(this)
          .closest("table")
          .find("tbody :checkbox:checked").length ==
          $(this)
            .closest("table")
            .find("tbody :checkbox").length
      );
  });

  // Editor texto
	//$('#summernote').summernote();

	// Drag and Drop cursos
//const agregadosList = document.querySelector("#agregadosList");

//document.addEventListener("dragstart", dragStartHandler, false);
//document.addEventListener("dragend", dragEndHandler, false);
//document.addEventListener("drop", dropHandler, false);

////agregadosList.addEventListener("dragover", dragOverHandler, false);

//let dragItem;
//let dropTarget;
//let sourceContainer;

//let dummyElement = document.createElement("li");
//dummyElement.className += "dummy";
//dummyElement.innerText = "Agregar";

//function dragStartHandler(event) {
//	dragItem = event.target;
//	sourceContainer = dragItem.parentNode;
//	dragItem.classList.add("active");
//}

//function dragOverHandler(event) {
//	event.preventDefault();

//	if (
//		event.target.nodeName === "UL" &&
//		event.target.dataset.type === dragItem.dataset.type
//	) {
//		event.dropEffect = "move";
//		dummyElement.classList.add("dummy");
//		event.target.append(dummyElement);
//		dropTarget = event.target;
//	} else {
//		event.dropEffect = "none";
//		dragItem.classList.remove("active");
//	}
//}

//function dragEndHandler(event) {
//	if (event.dataTransfer.dropEffect === "copy") {
//		if (dropTarget) {
//			removeDummyElement();
//			dropTarget.append(dragItem);
//			dragItem.classList.remove("active");
//			dropTarget = null;
//			dragItem = null;
//		}
//	} else {
//		dragItem.classList.remove("active");
//	}
//}

//function dropHandler(event) {
//	console.log("Droppped ....");
//}

//function removeDummyElement() {
//	if (dropTarget && dropTarget.contains(dummyElement)) {
//		dropTarget.removeChild(dummyElement);
//	}
//}


  // File Avatar Profesor
  	$("#avatar").on("change",function(){
    	$(this).parent().find(".file-value").remove();
    	var fileValue = this.value.replace(/\\/g, "/").split("/");
    	$(this).parent().append("<i class='file-value'>"+fileValue[fileValue.length - 1]+"</i>");
	});



    $('.list-tareas .btn-materiales').click(function() {
      $('.list-tabs-temario .nav-link').removeClass('active');
      $('.list-pane-temario .tab-pane.fade').removeClass('active show');
      $('.list-tabs-temario #materiales-tab').addClass('active');
      $('.list-pane-temario #materiales').addClass('active show');
    });

    $('.navbar-toggler').click(function() {
      $(this).toggleClass('active')
      $('.navbar-collapse').toggleClass('open');
    });

    $('#olvidoContrasena').click(function () {
        $("#login").hide();
        $('#olvidoPass').fadeIn();
        $(".pgn-wrapper").hide();
    });
    $('.enviar-encuesta').click(function() {
      $("#encuesta, .encuesta-bajada").hide();
      $('#gracias').fadeIn();
      return false;
    });

    $('.btn-upload').click(function() {
		$("#list-tareas-unidad").hide();
		
		let divTareaGuid = $(this).data("id")

		console.log(divTareaGuid)

		$(`#${divTareaGuid}`).fadeIn();
    });

    $('.cancelUpload').click(function() {
      $('.form-tareas').hide();
      $("#list-tareas-unidad").fadeIn();
    });

    $('.btn-ver-tareas').click(function() {
		$("#list-tareas-unidad").hide();

		let divTareaGuid = $(this).data("id")

		console.log(divTareaGuid)

		$(`#lista-${divTareaGuid}`).fadeIn();
    });

    // Input file tareas
    $("input[type='file']").on("change",function(){
	    $(this).parent().find(".file-value").remove();
	    var fileValue = this.value.replace(/\\/g, "/").split("/");
	    $(this).parent().append("<span class='file-value'>"+fileValue[fileValue.length - 1]+"</span>");
		upload_task(this, '#rutaTarea', '#message')
	});
   // Validar Form Contacto
   $(function () {
	  $.validator.setDefaults({
		  submitHandler: function (form) {
			  form.submit();
	      //alert( "Mensaje enviado!" );	      
	    }
	  });

	$('#contactForm').validate({
	    rules: {
	      email: {
	        required: true,
	        email: true
	      },
	      nombre: {
	        required: true
	      },
	      mensaje: {
	        required: true
	      },
	    },
	    messages: {
	      email: {
	        required: "Ingresa tu correo",
	        email: "Por favor ingresa un email válido"
	      },
	      nombre: {
	        required: "Ingresa tu nombre"
	      },
	      mensaje: "Ingresa tu mensaje"
	    },
	    errorElement: 'span',
	    errorPlacement: function (error, element) {
	      error.addClass('invalid-feedback');
	      element.closest('.form-group').append(error);
	    },
	    highlight: function (element, errorClass, validClass) {
	      $(element).addClass('is-invalid');
	    },
	    unhighlight: function (element, errorClass, validClass) {
	      $(element).removeClass('is-invalid');	      
	    }	    
	  });
	});

   	$(function () {
	  $.validator.setDefaults({
	    submitHandler: function () {
	      //alert( "Mensaje enviado!" );
			  form.submit();
	    }
	  });

	$('#registro').validate({
	    rules: {
	      tipoDoc: {
	        required: true
	      },
	      DNI: {
	        required: true,
	        minlength: 8,
	        maxlength: 9
	      },
		  Nombres: {
	        required: true	        
	      },
		  Apellidos: {
	        required: true
	      },
		 Email: {
	        required: true,
	        email: true
	      },
		 ConfirmarEmail: {
	        required: true,
	        email: true
	      },
		  Password: {
	        required: true
	      },
	      telefono: {
	        required: true,
	        minlength: 9
			},

			Region: {
				required: true
			},

			cboprovincia: {
				required: true
			},
			DistritoId: {
				required: true
			},
			direccion: {
				required: true
			},

			NacionalidadId2: {
				required: true
			},
			LstSexo: {
				required: true
			},

			LstSector: {
	        required: true
	      },
			RUC: {
	        required: true
	      },
			RazonSocial: {
	        required: true
	      },
			CIIU: {
	        required: true
	      },
			tyc: {
	        required: true
			},
			tyc2: {
				required: true
			}
	    },
	    messages: {
			tipoDoc: {
				required: "Selecciona un tipo"
			},
			DNI: {
				required: "Ingresa núm de doc.",
				minlength: "Número de doc. inválido",
				maxlength: "Número de doc. inválido"
			},
			Nombres: {
				required: "Ingresa tu(s) nombre(s)"
			},
			Apellidos: {
				required: "Ingresa tus apellidos"
			},
			Email: {
				required: "Ingresa tu correo electrónico",
				email: "Correo electrónico inválido"
			},
			ConfirmarEmail: {
				required: "Confirma tu correo electrónico",
				email: "El correo ingresado no coincide",
				equalTo: "email"
			},
			Password: {
				required: "Ingresa tu contraseña"
			},
			Region: {
				required: "Selecciona un departamento"
			},
			cboprovincia: {
				required: "Selecciona una provincia"
			},
			DistritoId: {
				required: "Selecciona un distrito"
			},
			direccion: {
				required: "Ingresa tu direccion"
			},
			telefono: {
				required: "Ingresa tu nro. de teléfono",
				minlength: "Número de telf. inválido"
			},
			NacionalidadId2: {
				required: "Selecciona un distrito"
			},
			LstSexo: {
				required: "Selecciona un distrito"
			},
			LstSector: {
				required: "Selecciona un sector"
			},
			RUC: {
				required: "Ingresa el RUC"
			},
			RazonSocial: {
				required: "Ingresa la razón social"
			},
			CIIU: {
				required: "Ingresa el cód. de la empresa"
			},
			tyc: {
				required: "Debes aceptar las politicas de privacidad"
			},
			tyc2: {
				required: "Debes aceptar los terminos y condiciones"
			}
	    },
		    errorElement: 'span',
		    errorPlacement: function (error, element) {
		      error.addClass('invalid-feedback');
		      element.closest('.form-group').append(error);		     
		    },
		    highlight: function (element, errorClass, validClass) {
		      $(element).addClass('is-invalid');
		      $('#errorGeneral').show();
		    },
		    unhighlight: function (element, errorClass, validClass) {
		      $(element).removeClass('is-invalid');
		      $('#errorGeneral').hide();
		      $('select').on('change', function(){
		      	$(this).removeClass('is-invalid');
		      });
		    }
		});
		$('input[name="tipoRegistro"]').on('change', function () {
	        if ($('#tipoEmpresa').is(':checked')) {
	        	console.log('Empresa');
	        	$('#datosEmpresa').show("slow");
	            $('#ruc').rules('add', {
	                required: true
	            });
	            $('#razonSocial').rules('add', {
	                required: true
	            });
	            $('#ciiu').rules('add', {
	                required: true
	            });
	        } else if ($('#tipoEmprendedor').is(':checked')){
	        	console.log('Emprendedor');
	        	$('#datosEmpresa').hide("slow");
	            $('#ruc').rules('add', {
	                required: false
	            });
	            $('#razonSocial').rules('add', {
	                required: false
	            });
	            $('#ciiu').rules('add', {
	                required: false
	            });
	        };
	        $('#ruc.error').each(function () {
	            $(this).valid();
	        });
	        $('#razonSocial.error').each(function () {
	            $(this).valid();
	        });
	        $('#ciiu.error').each(function () {
	            $(this).valid();
	        });
	    });		
	});


   	$('.slider-principal').slick({
		dots: true,
		infinite: false,
		slidesToShow: 1,
		slidesToScroll: 1,
		autoplay: false,
		fade: true,
		speed: 800,
  		autoplaySpeed: 6000,
  		arrows: true,
  		prevArrow: '<div class="slick-prev"><i class="fas fa-chevron-left"></i></div>',
		nextArrow: '<div class="slick-next"><i class="fas fa-chevron-right"></i></div>',
		responsive: [
	    {
	      breakpoint: 600,
	      settings: {
	        arrows: false,
	        dots: true,
			initialSlide: 0
	      }
	    }
	  ]
   	});

   	$(".filter li").on('click', function(){
	    var filter = $(this).data('filter');
	    $(".slider-cursos-home").slick('slickUnfilter');
	    
	    if(filter == 'mas-valorados'){
	    	$(".filter li").removeClass('active');
	    	$(this).addClass('active');
	      	$(".slider-cursos-home").slick('slickFilter','.mas-valorados');
	      	$('select.categorias').prop('selectedIndex',0);
	    }
	    else if(filter == 'mas-recientes'){
	    	$(".filter li").removeClass('active');
	    	$(this).addClass('active');
	      	$(".slider-cursos-home").slick('slickFilter','.mas-recientes');
	      	$('select.categorias').prop('selectedIndex',0);
	    }
	    else if(filter == 'eventos-webinar'){
	    	$(".filter li").removeClass('active');
	    	$(this).addClass('active');
	      	$(".slider-cursos-home").slick('slickFilter','.eventos-webinar');
	      	$('select.categorias').prop('selectedIndex',0);
	    }
	    else if(filter == 'todos'){
	    	$(".filter li").removeClass('active');
	    	$(this).addClass('active');	      
	      	$(".slider-cursos-home").slick('slickUnfilter');
	      	$('select.categorias').prop('selectedIndex',0);
	    }	    
	});

	$('select.categorias').on('change', function() {
	    var filterClass = getFilterValue();
	    $(".filter li").removeClass('active');
	    $('.slider-cursos-home').slick('slickUnfilter');
	    $('.slider-cursos-home').slick('slickFilter', '.'+filterClass);
	});

	function getFilterValue() {
	    // Grab all the values from the filters.
	    var values = $('.filter-group').map(function() {
	      // For each group, get the select values.
	      var groupVal = $(this).find('select').map(function() {
	        return $(this).val();
	      }).get();
	      // join the values together.
	      return groupVal.join('');
	    }).get();
	    // Remove empty strings from the filter array.
	    // and join together with a comma. this way you 
	    // can use multiple filters.
	    return values.filter(function(n) {
	      return n !== "";
	    }).join(',');
	  }
	  
	  /**
	   * Add a delete button to the filter group.
	   */
	  $('.filter-group .delete').on('click', function(event) {
	    event.preventDefault();
	    $(this).closest('.filter-group').remove();
	  });

	  $('.vista-lista').on('click', function(event) {
	  	$('.btn-tool').removeClass('active');
	  	$(this).addClass('active');
	    $('.slider-mis-cursos').hide();
	    $('.grid-items').hide();
	    $('.list-cards').show();
	  });

	  $('.vista-grid').on('click', function(event) {
	  	$('.btn-tool').removeClass('active');
	  	$(this).addClass('active');
	    $('.slider-mis-cursos').show();
	    $('.grid-items').show();
	    $('.list-cards').hide();
	  });

	  $('.notification.new').on('click', function(event) {
	  	$(this).removeClass('new');
	  });

   	$('.slider-cursos-home').slick({
		dots: true,
		infinite: false,
		slidesToShow: 4,
		slidesToScroll: 1,
		autoplay: false,
		speed: 800,
  		autoplaySpeed: 6000,
  		arrows: true,
  		prevArrow: '<div class="slick-prev"><i class="fas fa-chevron-left"></i></div>',
		nextArrow: '<div class="slick-next"><i class="fas fa-chevron-right"></i></div>',
		responsive: [
	    {
	      breakpoint: 600,
	      settings: {
	        slidesToShow: 1,
	        slidesToScroll: 1,
	        arrows: false,
	        dots: true,
	        centerMode: true,
			centerPadding: '30px',
			initialSlide: 0
	      }
	    }
	  ]
   	});

   	$('.slider-mis-cursos').slick({
		dots: true,
		infinite: true,
		slidesToShow: 4,
		slidesToScroll: 1,
		autoplay: false,
		speed: 800,
  		autoplaySpeed: 6000,
  		arrows: true,
  		centerMode: false,
		centerPadding: '0px',
  		prevArrow: '<div class="slick-prev"><i class="fas fa-chevron-left"></i></div>',
		nextArrow: '<div class="slick-next"><i class="fas fa-chevron-right"></i></div>',
		responsive: [
	    {
	      breakpoint: 1400,
	      settings: {
	        slidesToShow: 3,
	        slidesToScroll: 1
	      }
	    },
	    {
	      breakpoint: 600,
	      settings: {
	        slidesToShow: 1,
	        slidesToScroll: 1,
	        arrows: false,
	        dots: true,
	        centerMode: true,
			centerPadding: '30px',
			initialSlide: 0
	      }
	    }
	  ]
   	});

   // Next Section
   $('.ScrollBoton').on('click', function () {
        $('body, html').animate({
            scrollTop: $('.seccion.experiencia').offset().top - 60
        }, 'slow');

    });

   if(/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)){
        // true for mobile device
        // document.write("mobile device");
        $('.tabs-navigation').hide();
        $('.select-nav-evaluacion').show();
        $('#selectNavigation').change(function(){
        	$('.tab-pane.fade').removeClass('active show');
			    $('#v-' +$(this).val()).addClass('active show');
			  });
    } else{
        // false for not mobile device
        // document.write("not mobile device");
        $('.select-nav-evaluacion').hide();
        $('.tabs-navigation').show();
    }

});