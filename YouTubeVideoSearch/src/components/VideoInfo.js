import React from "react";
import "../styles/video-info.scss";

function VideoInfo(prop) {
  const video = prop.video;
  if (video) {
    const link = `https://youtube.com/watch?v=${video.id}`;
    return (
      <div className="video-info">
        <a href={link} className="video-info__link">
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
