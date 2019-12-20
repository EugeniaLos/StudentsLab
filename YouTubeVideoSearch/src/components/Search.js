import React, { Component } from "react";
import PropTypes from "prop-types";
import VideoInfo from "./VideoInfo";

class Search extends Component {
  onChange = evt => {
    this.props.setSearchText(evt.target.value);
  };

  fetchYoutubeVideo = evt => {
    evt.preventDefault();
    this.props.fetchYoutubeVideo(this.props.searchText);
  };

  render() {
    return (
      <div>
        <form className="search-form" onSubmit={this.fetchYoutubeVideo}>
          <input
            className="search-form__input-field"
            type="text"
            placeholder="Search"
            onChange={this.onChange}
          ></input>
          <button className="search-form__button" type="submit">
            Go!
          </button>
        </form>
        <div className="videos-container">
          <VideoInfo video={this.props.videos[0]} />
          <VideoInfo video={this.props.videos[1]} />
          <VideoInfo video={this.props.videos[2]} />
          <VideoInfo video={this.props.videos[3]} />
          <VideoInfo video={this.props.videos[4]} />
          <VideoInfo video={this.props.videos[5]} />
          <VideoInfo video={this.props.videos[6]} />
          <VideoInfo video={this.props.videos[7]} />
        </div>
      </div>
    );
  }
}

Search.propTypes = {
  searchText: PropTypes.string.isRequired,
  fetchYoutubeVideo: PropTypes.func.isRequired,
  setSearchText: PropTypes.func.isRequired,
  videos: PropTypes.array.isRequired
};

export default Search;
