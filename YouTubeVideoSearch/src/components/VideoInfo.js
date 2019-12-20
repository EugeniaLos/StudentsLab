import React from "react";

function VideoInfo(prop) {
  let video = prop.video;
  if (video != undefined) {
    let link = "https://youtube.com/watch?v=" + video.id;
    return (
      <div className="video-info">
        <a href={link}>
          <img src={video.thumbnails.url} className="video-info__img"></img>
          <div className="video-info__text-container">
            <h4 className="video-info__title">{video.title}</h4>
            <div className="video-info__description">{video.description}</div>
          </div>
        </a>
      </div>
    );
  }
  return null;
}

export default VideoInfo;
