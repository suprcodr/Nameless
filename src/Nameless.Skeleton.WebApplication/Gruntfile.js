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
                    cwd: './bower_components/bootstrap/dist',
                    src: ['fonts/**/*'],
                    dest: './wwwroot/assets/'
                }, {
                    expand: true,
                    cwd: './bower_components/bootstrap-colorpicker/dist',
                    src: ['img/**/*'],
                    dest: './wwwroot/assets/'
                }, {
                    expand: true,
                    cwd: './bower_components/bootstrap-fileinput',
                    src: ['img/**/*'],
                    dest: './wwwroot/assets/'
                }, {
                    expand: true,
                    cwd: './bower_components/fontawesome',
                    src: ['fonts/**/*'],
                    dest: './wwwroot/assets/'
                }, {
                    expand: true,
                    cwd: './bower_components/iCheck/skins/square',
                    src: ['**/*.png'],
                    dest: './wwwroot/assets/img'
                }, {
                    expand: true,
                    cwd: './bower_components/ion.rangeSlider',
                    src: ['img/**/*'],
                    dest: './wwwroot/assets/'
                }, {
                    expand: true,
                    cwd: './bower_components/Ionicons',
                    src: ['fonts/**/*'],
                    dest: './wwwroot/assets/'
                }]
            },

            ckeditor: {
                files: [{
                    expand: true,
                    cwd: './bower_components/ckeditor',
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
                    './bower_components/jquery/dist/jquery.min.js',
                    './bower_components/jquery-ui/jquery-ui.min.js',
                    './wwwroot/src/js/jquery-ui.fixture.min.js',
                    './bower_components/bootstrap/dist/js/bootstrap.min.js',
                    './bower_components/bootstrap-tokenfield/dist/bootstrap-tokenfield.min.js',
                    './bower_components/typeahead.js/dist/typeahead.min.js',
                    './bower_components/raphael/raphael.min.js',
                    './bower_components/morris.js/morris.min.js',
                    './bower_components/jquery-sparkline/dist/jquery.sparkline.min.js',
                    './bower_components/jvectormap/jquery.jvectormap.min.js',
                    './wwwroot/vendors/jvectormap/jquery-jvectormap-world-mill.min.js',
                    './bower_components/jquery-knob/dist/jquery.knob.min.js',
                    './bower_components/moment/min/moment-with-locales.min.js',
                    './bower_components/bootstrap-daterangepicker/daterangepicker.min.js',
                    './wwwroot/vendors/datepicker/bootstrap-datepicker.min.js',
                    './wwwroot/vendors/datepicker/locales/*.min.js',
                    './bower_components/bootstrap3-wysihtml5-bower/dist/bootstrap3-wysihtml5.all.min.js',
                    './bower_components/bootstrap3-wysihtml5-bower/dist/locales/*.min.js',
                    './bower_components/jquery-slimscroll/jquery.slimscroll.min.js',
                    './bower_components/fastclick/lib/fastclick.min.js',
                    './bower_components/iCheck/icheck.min.js',
                    './bower_components/bootstrap-fileinput/js/fileinput.min.js',
                    './bower_components/bootstrap-fileinput/js/locales/*.min.js',
                    './bower_components/bootbox/bootbox.min.js',
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
                    './bower_components/bootstrap/dist/css/bootstrap.min.css',
                    './bower_components/bootstrap-tokenfield/dist/css/bootstrap-tokenfield.min.css',
                    './bower_components/bootstrap-tokenfield/dist/css/tokenfield-typeahead.min.css',
                    './bower_components/fontawesome/css/font-awesome.min.css',
                    './bower_components/Ionicons/css/ionicons.min.css',
                    './wwwroot/vendors/AdminLTE/css/_imports.css',
                    './wwwroot/vendors/AdminLTE/css/AdminLTE.min.css',
                    './wwwroot/vendors/AdminLTE/css/skins/_all-skins.min.css',
                    './bower_components/iCheck/skins/square/_all.min.css',
                    './wwwroot/vendors/iCheck/css/square.override.min.css',
                    './bower_components/morris.js/morris.min.css',
                    './bower_components/jvectormap/jquery-jvectormap.min.css',
                    './wwwroot/vendors/datepicker/datepicker3.min.css',
                    './bower_components/bootstrap-daterangepicker/daterangepicker.min.css',
                    './bower_components/bootstrap3-wysihtml5-bower/dist/bootstrap3-wysihtml5.min.css',
                    './bower_components/bootstrap-fileinput/css/fileinput.min.css',
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
                    './bower_components/jquery/dist/jquery.min.js',
                    './bower_components/bootstrap/dist/js/bootstrap.min.js',
                    './bower_components/iCheck/icheck.min.js',
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
                    './bower_components/bootstrap/dist/css/bootstrap.min.css',
                    './bower_components/fontawesome/css/font-awesome.min.css',
                    './bower_components/Ionicons/css/ionicons.min.css',
                    './wwwroot/vendors/AdminLTE/css/_imports.css',
                    './wwwroot/vendors/AdminLTE/css/AdminLTE.min.css',
                    './bower_components/iCheck/skins/square/_all.min.css',
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
                    './bower_components/html5shiv/dist/html5shiv.min.js',
                    './bower_components/respond/dest/respond.min.js'
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
                    './bower_components/jquery-validation/dist/jquery.validate.min.js',
                    './wwwroot/src/validate.min.js',
                    './bower_components/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js'
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
                    
                    './bower_components/bootstrap-daterangepicker/daterangepicker.min.css': './bower_components/bootstrap-daterangepicker/daterangepicker.css',
                    './bower_components/bootstrap-timepicker/css/timepicker.min.css': './bower_components/bootstrap-timepicker/css/timepicker.css',
                    './bower_components/iCheck/skins/square/_all.min.css': './bower_components/iCheck/skins/square/_all.css',
                    './bower_components/ion.rangeSlider/css/ion.rangeSlider.skinHTML5.min.css': './bower_components/ion.rangeSlider/css/ion.rangeSlider.skinHTML5.css',
                    './bower_components/jvectormap/jquery-jvectormap.min.css': './bower_components/jvectormap/jquery-jvectormap.css',
                    './bower_components/morris.js/morris.min.css': './bower_components/morris.js/morris.css',
                    './bower_components/PACE/themes/blue/pace-theme-flash.min.css': './bower_components/PACE/themes/blue/pace-theme-flash.css'
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

                    './bower_components/bootbox/bootbox.min.js': './bower_components/bootbox/bootbox.js',
                    './bower_components/bootstrap-daterangepicker/daterangepicker.min.js': './bower_components/bootstrap-daterangepicker/daterangepicker.js',
                    './bower_components/bootstrap-timepicker/js/bootstrap-timepicker.min.js': './bower_components/bootstrap-timepicker/js/bootstrap-timepicker.js',
                    './bower_components/eve-raphael/eve.min.js': './bower_components/eve-raphael/eve.js',
                    './bower_components/fastclick/lib/fastclick.min.js': './bower_components/fastclick/lib/fastclick.js',
                    './bower_components/fullcalendar/dist/locale-all.min.js': './bower_components/fullcalendar/dist/locale-all.js',
                    './bower_components/jquery-flot/jquery.flot.min.js': './bower_components/jquery-flot/jquery.flot.js'
                }]
            },
            locale_wysihtml5: {
                files: grunt.file.expandMapping(['./bower_components/bootstrap3-wysihtml5-bower/dist/locales/*.js', '!./bower_components/bootstrap3-wysihtml5-bower/dist/locales/*.min.js'], './', {
                    rename: function (destinationBase, destinationPath) {
                        return destinationBase + destinationPath.replace('.js', '.min.js');
                    }
                })
            },
            locale_fullcalendar: {
                files: grunt.file.expandMapping(['./bower_components/fullcalendar/dist/locale/*.js', '!./bower_components/fullcalendar/dist/locale/*.min.js'], './', {
                    rename: function (destinationBase, destinationPath) {
                        return destinationBase + destinationPath.replace('.js', '.min.js');
                    }
                })
            },
            locale_select2: {
                files: grunt.file.expandMapping(['./bower_components/select2/dist/js/i18n/*.js', '!./bower_components/select2/dist/js/i18n/*.min.js'], './', {
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
                files: grunt.file.expandMapping(['./bower_components/bootstrap-fileinput/js/locales/*.js', '!./bower_components/bootstrap-fileinput/js/locales/*.min.js'], './', {
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
                    './bower_components/bootstrap-timepicker/css/timepicker.css': './bower_components/bootstrap-timepicker/css/timepicker.less'
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