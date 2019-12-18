import { connect } from "redux";
import Search from "../components/Search";

import { fetchVideo } from "../actions/actions";

const mapStateToProps = state => ({
  searchText: state.search.searchText
});

const mapDispatchToProps = dispatch => ({
  fetchYoutubeVideo: searchText => fetchVideo(searchText)
});

connect(mapStateToprops, mapDispatchToProps)(Search);
