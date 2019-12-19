import React from "react";

function VideoInfo(prop) {
  let video = prop.video;
  if (video != undefined) {
    let link = "https://youtube.com/watch?v=" + video.id;
    return (
      <a href={link}>
        <img src={video.thumbnails.url} className="video-info__img"></img>
      </a>
    );
  }
  return null;
}

export default VideoInfo;
