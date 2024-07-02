#!/bin/bash
set -e
set -o pipefail
shopt -s nullglob

# CD to script directory
cd "$(dirname "$0")"
# Store absolute path
SCRIPT_PATH="$(pwd)"

DEST_PATH=$1

if [[ $DEST_PATH == "" ]]; then
    echo "Usage: $0 <path to SortByRawName or plugins folder> [docker image]"
    exit 1
fi

DOCKER_IMAGE=$2

# If "SortByRawName" not in path name, try to find it in the given folder
if [[ $DEST_PATH != *"SortByRawName"* ]]; then
    echo "Searching for SortByRawName folder in $DEST_PATH"
    cd "$DEST_PATH"
    VERSION_NUM="$(echo SortByRawName* | sed 's/SortByRawName_//g' | sed 's/ /\n/g' | sort -V | tac | head -n 1)"
    # If we didn't find anything, just pretend dest is the SortByRawName folder
    if [[ $VERSION_NUM == "" ]]; then
        echo "No SortByRawName folder found in $DEST_PATH, assuming this is the SortByRawName folder"
    else
        DEST_PATH="$DEST_PATH/SortByRawName_$VERSION_NUM"
        echo "Found SortByRawName folder at $DEST_PATH"
    fi
fi

# If $DOCKER_IMAGE is not set, build it
if [[ $DOCKER_IMAGE == "" ]]; then
    echo "Building docker image"
    docker build -t jellyfin-sort-by-raw-name-plugin-build "$SCRIPT_PATH"
    DOCKER_IMAGE="jellyfin-sort-by-raw-name-plugin-build"
fi

docker run --rm -v "$DEST_PATH":/out $DOCKER_IMAGE

docker rmi $DOCKER_IMAGE
