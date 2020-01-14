import { connect } from "react-redux";
import Search from "../components/Search";

import { fetchVideo, changeSearchText } from "../actions/actions";

const mapStateToProps = state => ({
  searchText: state.searchText,
  videos: state.videos
});

const mapDispatchToProps = dispatch => ({
  fetchYoutubeVideo: searchText => dispatch(fetchVideo(searchText)),
  setSearchText: searchText => dispatch(changeSearchText(searchText))
});

export default connect(mapStateToProps, mapDispatchToProps)(Search);
