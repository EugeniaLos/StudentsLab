const KEY = "AIzaSyCE2ZC8f6cw-dk26VVVYSVpDzt_EKBhwzY";

export const fetchYoutubeVideo = searchString =>
  fetch(
    `https://www.googleapis.com/youtube/v3/search?part=snippet&maxResults=8&key=${KEY}&q=${searchString}&type=video`
  );