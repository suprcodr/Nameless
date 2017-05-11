module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('./package.json'),

        // JSHint is a tool that helps to detect errors and potential problems in JavaScript code.
        jshint: {
            // define the files to lint
            files: [
                './Gruntfile.js',
                './wwwroot/src/**/*.js',
                '!./wwwroot/src/**/*.min.js',
                './wwwroot/test/**/*.js',
                '!./wwwroot/test/**/*.min.js',

                // Bookshelf
                './Areas/Bookshelf/wwwroot/**/*.js',
                '!./Areas/Bookshelf/wwwroot/**/*.min.js'
            ],
            // configure JSHint (documented at http://www.jshint.com/docs/)
            options: {
                // more options here if you want to override JSHint defaults
                globals: {
                    jQuery: true,
                    console: true,
                    module: true
                }
            }
        },

        // QUnit is a powerful, easy-to-use JavaScript unit testing framework.
        qunit: {
            files: [
                './wwwroot/test/**/*.html'
            ]
        },

        // Copy all necessary resources
        copy: {
            application: {
                files: [{
                    expand: true,
                    cwd: './wwwroot/src/',
                    src: ['img/**/*'],
                    dest: './wwwroot/assets/'
                }]
            },

            vendors: {
                files: [{
                    expand: true,
                    cwd: './wwwroot/vendors/AdminLTE',
                    src: ['img/**/*'],
                    dest: './wwwroot/assets/'
                }]
            },

            plugins: {
                files: [{
                    expand: true,
                    cwd: './wwwroot/lib/bootstrap/dist',
                    src: ['fonts/**/*'],
                    dest: './wwwroot/assets/'
                }, {
                    expand: true,
                    cwd: './wwwroot/lib/bootstrap-colorpicker/dist',
                    src: ['img/**/*'],
                    dest: './wwwroot/assets/'
                }, {
                    expand: true,
                    cwd: './wwwroot/lib/bootstrap-fileinput',
                    src: ['img/**/*'],
                    dest: './wwwroot/assets/'
                }, {
                    expand: true,
                    cwd: './wwwroot/lib/fontawesome',
                    src: ['fonts/**/*'],
                    dest: './wwwroot/assets/'
                }, {
                    expand: true,
                    cwd: './wwwroot/lib/iCheck/skins/square',
                    src: ['**/*.png'],
                    dest: './wwwroot/assets/img'
                }, {
                    expand: true,
                    cwd: './wwwroot/lib/ion.rangeSlider',
                    src: ['img/**/*'],
                    dest: './wwwroot/assets/'
                }, {
                    expand: true,
                    cwd: './wwwroot/lib/Ionicons',
                    src: ['fonts/**/*'],
                    dest: './wwwroot/assets/'
                }]
            },

            ckeditor: {
                files: [{
                    expand: true,
                    cwd: './wwwroot/lib/ckeditor',
                    src: [
                        'lang/**/*.js',
                        'plugins/**/*',
                        'skins/**/*',
                        'ckeditor.js',
                        'config.js',
                        'styles.js'
                    ],
                    dest: './wwwroot/assets/ckeditor'
                }]
            },

            bookshelf: {
                files: [{
                    expand: true,
                    cwd: './Areas/Bookshelf/wwwroot/',
                    src: ['css/**/*', 'img/**/*', 'js/**/*'],
                    dest: './wwwroot/assets/areas/bookshelf'
                }]
            }
        },

        // Concatenate JavaScript files.
        concat: {
            bundle_js: {
                options: {
                    separator: ';' /* define a string to put between each file in the concatenated output */
                },
                // the files to concatenate
                src: [
                    // COMMON
                    './wwwroot/lib/jquery/dist/jquery.min.js',
                    './wwwroot/lib/jquery-ui/jquery-ui.min.js',
                    './wwwroot/src/js/jquery-ui.fixture.min.js',
                    './wwwroot/lib/bootstrap/dist/js/bootstrap.min.js',
                    './wwwroot/lib/bootstrap-tokenfield/dist/bootstrap-tokenfield.min.js',
                    './wwwroot/lib/typeahead.js/dist/typeahead.min.js',
                    './wwwroot/lib/raphael/raphael.min.js',
                    './wwwroot/lib/morris.js/morris.min.js',
                    './wwwroot/lib/jquery-sparkline/dist/jquery.sparkline.min.js',
                    './wwwroot/lib/jvectormap/jquery.jvectormap.min.js',
                    './wwwroot/vendors/jvectormap/jquery-jvectormap-world-mill.min.js',
                    './wwwroot/lib/jquery-knob/dist/jquery.knob.min.js',
                    './wwwroot/lib/moment/min/moment-with-locales.min.js',
                    './wwwroot/lib/bootstrap-daterangepicker/daterangepicker.min.js',
                    './wwwroot/vendors/datepicker/bootstrap-datepicker.min.js',
                    './wwwroot/vendors/datepicker/locales/*.min.js',
                    './wwwroot/lib/bootstrap3-wysihtml5-bower/dist/bootstrap3-wysihtml5.all.min.js',
                    './wwwroot/lib/bootstrap3-wysihtml5-bower/dist/locales/*.min.js',
                    './wwwroot/lib/jquery-slimscroll/jquery.slimscroll.min.js',
                    './wwwroot/lib/fastclick/lib/fastclick.min.js',
                    './wwwroot/lib/iCheck/icheck.min.js',
                    './wwwroot/lib/bootstrap-fileinput/js/fileinput.min.js',
                    './wwwroot/lib/bootstrap-fileinput/js/locales/*.min.js',
                    './wwwroot/lib/bootbox/bootbox.min.js',
                    './wwwroot/vendors/AdminLTE/js/AdminLTE.min.js',
                    // CUSTOM
                    './wwwroot/src/js/checkbox.min.js',
                    './wwwroot/src/js/classie.min.js',
                    './wwwroot/src/js/form.min.js',
                    './wwwroot/src/js/polyfills.min.js',
                    './wwwroot/src/js/utilities.min.js'
                ],
                // the location of the resulting JS file
                dest: './wwwroot/assets/js/<%= pkg.name %>.bundle.min.js'
            },
            bundle_css: {
                options: {
                    separator: '\n' /* define a string to put between each file in the concatenated output */
                },
                // the files to concatenate
                src: [
                    // COMMON
                    './wwwroot/lib/bootstrap/dist/css/bootstrap.min.css',
                    './wwwroot/lib/bootstrap-tokenfield/dist/css/bootstrap-tokenfield.min.css',
                    './wwwroot/lib/bootstrap-tokenfield/dist/css/tokenfield-typeahead.min.css',
                    './wwwroot/lib/fontawesome/css/font-awesome.min.css',
                    './wwwroot/lib/Ionicons/css/ionicons.min.css',
                    './wwwroot/vendors/AdminLTE/css/_imports.css',
                    './wwwroot/vendors/AdminLTE/css/AdminLTE.min.css',
                    './wwwroot/vendors/AdminLTE/css/skins/_all-skins.min.css',
                    './wwwroot/lib/iCheck/skins/square/_all.min.css',
                    './wwwroot/vendors/iCheck/css/square.override.min.css',
                    './wwwroot/lib/morris.js/morris.min.css',
                    './wwwroot/lib/jvectormap/jquery-jvectormap.min.css',
                    './wwwroot/vendors/datepicker/datepicker3.min.css',
                    './wwwroot/lib/bootstrap-daterangepicker/daterangepicker.min.css',
                    './wwwroot/lib/bootstrap3-wysihtml5-bower/dist/bootstrap3-wysihtml5.min.css',
                    './wwwroot/lib/bootstrap-fileinput/css/fileinput.min.css',
                    // CUSTOM
                    './wwwroot/src/css/form.min.css',
                    './wwwroot/src/css/form.override.min.css',
                    './wwwroot/src/css/widget.min.css'
                ],
                // the location of the resulting CSS file
                dest: './wwwroot/assets/css/<%= pkg.name %>.bundle.min.css'
            },
            minimal_js: {
                options: {
                    separator: ';' /* define a string to put between each file in the concatenated output */
                },
                // the files to concatenate
                src: [
                    // COMMON
                    './wwwroot/lib/jquery/dist/jquery.min.js',
                    './wwwroot/lib/bootstrap/dist/js/bootstrap.min.js',
                    './wwwroot/lib/iCheck/icheck.min.js',
                    // CUSTOM
                    './wwwroot/src/js/checkbox.min.js',
                    './wwwroot/src/js/classie.min.js',
                    './wwwroot/src/js/form.min.js',
                    './wwwroot/src/js/polyfills.min.js'
                ],
                // the location of the resulting JS file
                dest: './wwwroot/assets/js/<%= pkg.name %>.minimal.min.js'
            },
            minimal_css: {
                options: {
                    separator: '\n' /* define a string to put between each file in the concatenated output */
                },
                // the files to concatenate
                src: [
                    // COMMON
                    './wwwroot/lib/bootstrap/dist/css/bootstrap.min.css',
                    './wwwroot/lib/fontawesome/css/font-awesome.min.css',
                    './wwwroot/lib/Ionicons/css/ionicons.min.css',
                    './wwwroot/vendors/AdminLTE/css/_imports.css',
                    './wwwroot/vendors/AdminLTE/css/AdminLTE.min.css',
                    './wwwroot/lib/iCheck/skins/square/_all.min.css',
                    './wwwroot/vendors/iCheck/css/square.override.min.css',
                    // CUSTOM
                    './wwwroot/src/css/form.min.css',
                    './wwwroot/src/css/form.override.min.css',
                    './wwwroot/src/css/widget.min.css'
                ],
                // the location of the resulting CSS file
                dest: './wwwroot/assets/css/<%= pkg.name %>.minimal.min.css'
            },
            ie8html5support: {
                options: {
                    separator: ';' /* define a string to put between each file in the concatenated output */
                },
                // the files to concatenate
                src: [
                    // COMMON
                    './wwwroot/lib/html5shiv/dist/html5shiv.min.js',
                    './wwwroot/lib/respond/dest/respond.min.js'
                ],
                // the location of the resulting JS file
                dest: './wwwroot/assets/js/<%= pkg.name %>.IE8.html5.min.js'
            },
            validation_js: {
                options: {
                    separator: ';' /* define a string to put between each file in the concatenated output */
                },
                // the files to concatenate
                src: [
                    './wwwroot/lib/jquery-validation/dist/jquery.validate.min.js',
                    './wwwroot/src/validate.min.js',
                    './wwwroot/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js'
                ],
                // the location of the resulting JS file
                dest: './wwwroot/assets/js/<%= pkg.name %>.validation.min.js'
            },

            bookshelf_js: {
                options: {
                    separator: ';' /* define a string to put between each file in the concatenated output */
                },
                // the files to concatenate
                src: [
                    './Areas/Bookshelf/wwwroot/js/'
                ],
                // the location of the resulting JS file
                dest: './wwwroot/assets/areas/bookshelf/js/bookshelf.bundle.min.js'
            },

            bookshelf_css: {
                options: {
                    separator: '\n' /* define a string to put between each file in the concatenated output */
                },
                // the files to concatenate
                src: [
                    './Areas/Bookshelf/wwwroot/css/'
                ],
                // the location of the resulting CSS file
                dest: './wwwroot/assets/areas/bookshelf/css/bookshelf.bundle.min.css'
            }
        },

        // Minify all css
        cssmin: {
            options: {
                shorthandCompacting: false,
                roundingPrecision: -1
            },
            build: {
                files: {
                    './wwwroot/src/css/form.min.css': './wwwroot/src/css/form.css',
                    './wwwroot/src/css/form.override.min.css': './wwwroot/src/css/form.override.css',
                    './wwwroot/src/css/widget.min.css': './wwwroot/src/css/widget.css',

                    './wwwroot/vendors/AdminLTE/css/AdminLTE.min.css': './wwwroot/vendors/AdminLTE/css/AdminLTE.css',
                    './wwwroot/vendors/AdminLTE/css/skins/_all-skins.min.css': './wwwroot/vendors/AdminLTE/css/skins/_all-skins.css',
                    './wwwroot/vendors/datepicker/datepicker3.min.css': './wwwroot/vendors/datepicker/datepicker3.css',
                    './wwwroot/vendors/iCheck/square.override.min.css': './wwwroot/vendors/iCheck/square.override.css',
                    
                    './wwwroot/lib/bootstrap-daterangepicker/daterangepicker.min.css': './wwwroot/lib/bootstrap-daterangepicker/daterangepicker.css',
                    './wwwroot/lib/bootstrap-timepicker/css/timepicker.min.css': './wwwroot/lib/bootstrap-timepicker/css/timepicker.css',
                    './wwwroot/lib/iCheck/skins/square/_all.min.css': './wwwroot/lib/iCheck/skins/square/_all.css',
                    './wwwroot/lib/ion.rangeSlider/css/ion.rangeSlider.skinHTML5.min.css': './wwwroot/lib/ion.rangeSlider/css/ion.rangeSlider.skinHTML5.css',
                    './wwwroot/lib/jvectormap/jquery-jvectormap.min.css': './wwwroot/lib/jvectormap/jquery-jvectormap.css',
                    './wwwroot/lib/morris.js/morris.min.css': './wwwroot/lib/morris.js/morris.css',
                    './wwwroot/lib/PACE/themes/blue/pace-theme-flash.min.css': './wwwroot/lib/PACE/themes/blue/pace-theme-flash.css'
                }
            },
            bookshelf: {
                files: {
                    './Areas/Bookshelf/wwwroot/css/book.views.show.min.css': './Areas/Bookshelf/wwwroot/css/book.views.show.css'
                }
            }
        },

        uglify: {
            options: {
                // the banner is inserted at the top of the output
                banner: '/*! <%= pkg.name %> <%= grunt.template.today("yyyy-mm-dd") %> */\n',
                mangle: false
            },
            dist: {
                files: [{
                    './wwwroot/src/js/checkbox.min.js': './wwwroot/src/js/checkbox.js',
                    './wwwroot/src/js/classie.min.js': './wwwroot/src/js/classie.js',
                    './wwwroot/src/js/form.min.js': './wwwroot/src/js/form.js',
                    './wwwroot/src/js/jquery-ui.fixture.min.js': './wwwroot/src/js/jquery-ui.fixture.js',
                    './wwwroot/src/js/polyfills.min.js': './wwwroot/src/js/polyfills.js',
                    './wwwroot/src/js/utilities.min.js': './wwwroot/src/js/utilities.js',
                    './wwwroot/src/js/validate.min.js': './wwwroot/src/js/validate.js',

                    './wwwroot/vendors/AdminLTE/js/AdminLTE.min.js': './wwwroot/vendors/AdminLTE/js/AdminLTE.js',
                    './wwwroot/vendors/AdminLTE/js/dashboard.min.js': './wwwroot/vendors/AdminLTE/js/dashboard.js',
                    './wwwroot/vendors/AdminLTE/js/demo.min.js': './wwwroot/vendors/AdminLTE/js/demo.js',
                    './wwwroot/vendors/datepicker/bootstrap-datepicker.min.js': './wwwroot/vendors/datepicker/bootstrap-datepicker.js',
                    './wwwroot/vendors/jvectormap/jquery.jvectormap.min.js': './wwwroot/vendors/jvectormap/jquery.jvectormap.js',
                    './wwwroot/vendors/jvectormap/jquery-jvectormap-world-mill.min.js': './wwwroot/vendors/jvectormap/jquery-jvectormap-world-mill.js',

                    './wwwroot/lib/bootbox/bootbox.min.js': './wwwroot/lib/bootbox/bootbox.js',
                    './wwwroot/lib/bootstrap-daterangepicker/daterangepicker.min.js': './wwwroot/lib/bootstrap-daterangepicker/daterangepicker.js',
                    './wwwroot/lib/bootstrap-timepicker/js/bootstrap-timepicker.min.js': './wwwroot/lib/bootstrap-timepicker/js/bootstrap-timepicker.js',
                    './wwwroot/lib/eve-raphael/eve.min.js': './wwwroot/lib/eve-raphael/eve.js',
                    './wwwroot/lib/fastclick/lib/fastclick.min.js': './wwwroot/lib/fastclick/lib/fastclick.js',
                    './wwwroot/lib/fullcalendar/dist/locale-all.min.js': './wwwroot/lib/fullcalendar/dist/locale-all.js',
                    './wwwroot/lib/jquery-flot/jquery.flot.min.js': './wwwroot/lib/jquery-flot/jquery.flot.js'
                }]
            },
            locale_wysihtml5: {
                files: grunt.file.expandMapping(['./wwwroot/lib/bootstrap3-wysihtml5-bower/dist/locales/*.js', '!./wwwroot/lib/bootstrap3-wysihtml5-bower/dist/locales/*.min.js'], './', {
                    rename: function (destinationBase, destinationPath) {
                        return destinationBase + destinationPath.replace('.js', '.min.js');
                    }
                })
            },
            locale_fullcalendar: {
                files: grunt.file.expandMapping(['./wwwroot/lib/fullcalendar/dist/locale/*.js', '!./wwwroot/lib/fullcalendar/dist/locale/*.min.js'], './', {
                    rename: function (destinationBase, destinationPath) {
                        return destinationBase + destinationPath.replace('.js', '.min.js');
                    }
                })
            },
            locale_select2: {
                files: grunt.file.expandMapping(['./wwwroot/lib/select2/dist/js/i18n/*.js', '!./wwwroot/lib/select2/dist/js/i18n/*.min.js'], './', {
                    rename: function (destinationBase, destinationPath) {
                        return destinationBase + destinationPath.replace('.js', '.min.js');
                    }
                })
            },
            locale_datepicker: {
                files: grunt.file.expandMapping(['./wwwroot/vendors/datepicker/locales/*.js', '!./wwwroot/vendors/datepicker/locales/*.min.js'], './', {
                    rename: function (destinationBase, destinationPath) {
                        return destinationBase + destinationPath.replace('.js', '.min.js');
                    }
                })
            },
            locale_fileinput: {
                files: grunt.file.expandMapping(['./wwwroot/lib/bootstrap-fileinput/js/locales/*.js', '!./wwwroot/lib/bootstrap-fileinput/js/locales/*.min.js'], './', {
                    rename: function (destinationBase, destinationPath) {
                        return destinationBase + destinationPath.replace('.js', '.min.js');
                    }
                })
            },

            bookshelf: {
                files: [{
                    //'./Areas/Bookshelf/wwwroot/js/*.min.js': './Areas/Bookshelf/wwwroot/*.js'
                }]
            }
        },

        watch: {
            files: ['<%= jshint.files %>'],
            tasks: ['jshint']
        },

        // This task will compile all less files upon saving to create *.css
        less: {
            build: {
                options: {
                    compress: false
                },
                files: {
                    './wwwroot/lib/bootstrap-timepicker/css/timepicker.css': './wwwroot/lib/bootstrap-timepicker/css/timepicker.less'
                }
            }
        },
    });

    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-contrib-less');
    grunt.loadNpmTasks('grunt-contrib-qunit');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-force-task');

    // Watch
    grunt.registerTask('watch', ['watch']);

    // this would be run by typing 'grunt test' on the command line
    grunt.registerTask('test', ['jshint', 'qunit']);

    // the default task can be run just by typing 'grunt' on the command line
    grunt.registerTask('default', ['force:jshint', 'force:qunit', 'less', 'cssmin', 'uglify', 'concat', 'copy']);
};