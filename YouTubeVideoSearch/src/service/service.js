const KEY = "AIzaSyCE2ZC8f6cw-dk26VVVYSVpDzt_EKBhwzY";

export const fetchYoutubeVideo = searchString =>
  fetch(
    `https://www.googleapis.com/youtube/v3/search?part=snippet&key=${KEY}&q=${searchString}&type=video`
  );

export const fetchNextYoutubeVideo = (searchString, nextPageToken) =>
  fetch(
    `https://www.googleapis.com/youtube/v3/search?part=snippet&key=${KEY}&pageToken=${nextPageToken}&q=${searchString}&type=video`
  );
