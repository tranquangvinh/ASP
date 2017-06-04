/* ----------------------------------------------------
 *  Core
 *
 *  This is the core javascript file for the application
 *  
 *  Description: Todo app is exclusively for XPLOIT
 * ---------------------------------------------------- */


/*global
    jQuery
*/


(function ($) {
	'use strict';

	/*
	 * App Information
	 * Contains all the basic information about Xploit
	 */
	var Xploit = function () {
		this.Version = '1.0.0';
		this.Author = 'Absolute Pixels';
		this.$html = $('html');
		this.$body = $('body');
	};

	/* ------------------------- 
		Begin Sidebar
	 ------------------------- */

	/**
	 * Function: initSidebarSparkline
	 * Initializes the sparklike in the sidebar
	 */
	Xploit.prototype.initSidebarSparkline = function () {
		/**
		 * jQuery sparkline - Uses jquery-sparkline.js to create
		 * the pie chart in the sidebar
		 */
		$('.sidebar-stats-sparkline').sparkline([526, 97], {
			type: 'pie',                        // specify the type of the chart - [text]
			width: '125',                       // width of the chart            - [int]
			height: '125',                      // height of the chart           - [int]
			sliceColors: ['#f93926', '#fff'],    // pie slice colors              - [array of colors]
			borderWidth: 0,                     // pie border width              - [int]
			borderColor: '#f93926'              // pie border color              - [text]
		});
	};

	/**
	 * Function: initSidebarNavigation
	 * Adds a class named 'open' to the <li> parent item
	 * of the <a> tag
	 */
	Xploit.prototype.initSidebarNavigation = function () {
		/**
		 * jQuery on click function is called to add the class on the <li>
		 * parent tag on the click of <a> tag.
		 */
		$('[data-toggle="nav-submenu"]').on('click', function (e) {
			e.stopPropagation();                // Prevent the default behaviour
			var $link = $(this);           // Get the current <a> tag
			var $parentLi = $link.parent('li'); // Get the parent of the <a> tag

			if ($parentLi.hasClass('open')) {   // Check if 'open' is already added to the <li>
				$parentLi.removeClass('open');  // Remove the class if added (close the accordion menu)
			} else {                            // .. else if submenu is closed, close all other (same level) submenus first before open it
				$link.closest('ul')             // Find the closes ul > li and remove the class
					.find('> li')
					.removeClass('open');

				$parentLi.addClass('open');     // Add the class 'open' to the <li>
			}
		});

		$('.scrollbar-padder').mCustomScrollbar({
            autoHideScrollbar: true,
            alwaysShowScrollBar: 0,
            scrollInertia: 60
        });
	};

	/* ------------------------- 
		End Sidebar
	 ------------------------- */



	/* ------------------------- 
		Begin Helpers
	 ------------------------- */

	/**
	 * Function: initToggleClass
	 * To toggle class using the data property of HTML by passing
	 * two data attributes on the toggle element
	 * Example: <a href="javascript:void(0)" data-toggle-class="hide" data-target=".sidebar-avatar-info"></a>
	 * Will toggle the class hide on .sidebar-avarar-info
	 */
	Xploit.prototype.initToggleClass = function () {

		/**
		 * Function to toggle class from HTML
		 * by using the two attritbutes
		 * 'data-toggle-class' & 'data-target'
		 *
		 * data-toggle-class = Class that needs to be toggled
		 * data-target = Target elements that need to toggled with
		 */
		$('[data-toggle-class]').on('click', function (e) {
			var __this = $(this);              // store the current element
			e.preventDefault();                // Prevent the default behaviour

			var x = [];

			// Store the classes & target elements in array
			var classes = __this.data('toggle-class').split(','),
				targets = (__this.data('target') && __this.data('target').split(',')) || x(__this),
				key = 0;


			$.each(classes, function (index, value) {
				var target = targets[(targets.length && key)];  // Store all the classes inside target variable

				// ( value.indexOf( '*' ) !== -1 );
				$(target).toggleClass(value);
				key ++;
			});

		});

	};

	/* ------------------------- 
		End Helpers
	 ------------------------- */



	/* ------------------------- 
		Begin Forms
	 ------------------------- */

	/**
     * The below function is used for animating
     * the input fields by adding and removing the
     * class input--filled and the markup stays as default
     * as the bootstrap markup, one extra class needs to be added
     * along with the form-group i.e., form-group-default
	 */

	Xploit.prototype.initInputFields = function () {
		// Check if the input is not empty
		// and add the class input--filled on page load
		$('.form-group-default input').each(function () {
			if ($(this).val() !== '') {
				$(this).parent().addClass('input--filled');
			}
		});

		// Delegate function on focus
		$('body').delegate('.form-group-default input', 'focus', function () {
			// Add class input--filled to the parent element
			$(this).parent().addClass('input--filled');

			// Check if the value is not null
			// and add the class input--filled
			if ($(this).val() !== '') {
				$(this).parent().addClass('input--filled');
			}


		// Delegate function on blur
		}).delegate('.form-group-default input', 'blur', function () {
			// Check if the value is not null
			// and add the class input--filled
			if ($(this).val() !== '') {
				// Add class
				$(this).parent().addClass('input--filled');
			} else {
				// Remove class
				$(this).parent().removeClass('input--filled');
			}
		});
	};

	/* ------------------------- 
		End Forms
	 ------------------------- */



	/* ------------------------- 
		Begin Tooltip & Popover
	 ------------------------- */

	// Initiate tooltip funciton
	Xploit.prototype.initTooltip = function () {
		$('[data-toggle="tooltip"]').tooltip();
	};


	// Initiate popover funciton
	Xploit.prototype.initPopover = function () {
		$('[data-toggle="popover"]').popover();
	};

	/* ------------------------- 
		End Tooltip & Popover
	 ------------------------- */


	/**
	 * Function: init
	 * Initialize all the functions used for the application
	 */
	Xploit.prototype.init = function () {
		this.initSidebarSparkline();
		this.initSidebarNavigation();
		this.initToggleClass();
		this.initInputFields();
		this.initTooltip();
		this.initPopover();
	};

	$.Xploit = new Xploit();
	$.Xploit.Constructor = Xploit;
})(jQuery);


/**
 * Call the init function to initialize all the functions for the application
 */
(function ($) {
	'use strict';

	// Initiate the core function
	$.Xploit.init();

	// Remove the class navbar-collapsed if available
	if (window.matchMedia('(max-width: 768px)').matches) {
        $('body').removeClass('navbar-collapsed');
    }
})(jQuery);
