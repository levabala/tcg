branch=${2:-development}

git add ./
git commit -m "$1"
git push origin "$branch"