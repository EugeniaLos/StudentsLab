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
{
  /* <Search searchText={state.searchText} fetchYoutubeVideo={searchText => dispatch(fetchVideo(searchText))}></Search>


const connect(mapStateToProps, mapDispatchToProps) => {
    const newprops = {};
    mapStateToProps.forEach(stateElem => props.push(stateElem))


    return newprops, () 
}

const connectedFoo = connect(mapStateToProps, mapDispatchToProps); // return props

connectedFoo(Search); // props --> search component's props

<Search searchText={newProps.searchText} /> */
}
