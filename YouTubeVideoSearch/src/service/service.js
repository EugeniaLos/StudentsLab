export const fetchYoutubeVideo = searchString =>
  fetch(`https://localhost:44309/video/${searchString}`);
