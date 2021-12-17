echo $(pwd)
find -mindepth 1 -maxdepth 1 -type d -print0 | xargs -r0 rm -R