var gulp = require('gulp'),
    sass = require('gulp-ruby-sass'),
    notify = require("gulp-notify"),
    concat = require('gulp-concat'),
    //rename = require('gulp-rename'),
    uglify = require('gulp-uglify'),
    jshint = require('gulp-jshint');

var path = {
    lib: './node_modules',
    custom: './Resources',
    public_css: './Content',
    public_js: './Scripts'
}

//gulp.task('icons', function () {
//    return gulp.src(path.lib + '/fontawesome/fonts/**.*')
//        .pipe(gulp.dest('./fonts'));
//});

gulp.task('sass', function () {
    return sass(path.custom + '/sass/*.scss', {
        style: 'compressed',
        loadPath: path.lib
    }).on("error", notify.onError(function (error) {
        return "Error: " + error.message;
    }))
        .pipe(concat('style.min.css'))
        .pipe(gulp.dest(path.public_css));
});

gulp.task('js', function () {
    return gulp.src([
        path.custom + '/js/*.js',
        path.lib + '/bootstrap-sass/assets/javascripts/bootstrap.js'
    ])
        .pipe(concat('script.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest(path.public_js));
});

gulp.task('jshint', function () {
    return gulp.src(path.public_js + '/js/**/*.js')
        .pipe(jshint())
        .pipe(jshint.reporter('jshint-stylish'));
});

gulp.task('watch', function () {
    gulp.watch(path.custom + '/sass/**/*.scss', ['sass']);
    gulp.watch(path.custom + '/js/**/*.js', ['js']);
    gulp.watch(path.custom + '/js/**/*.js', ['jshint']);
});

gulp.task('default', ['sass', 'js', 'jshint']);