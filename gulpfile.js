var gulp = require("gulp");
var sass = require("gulp-sass");

var paths = {
    webrootStatic: "./wwwroot/static"
}

gulp.task("sass", function () {
    return gulp.src(paths.webrootStatic + "/css/StyleSheet.scss")
        .pipe(sass())
        .pipe(gulp.dest(paths.webrootStatic + "/css"));
})