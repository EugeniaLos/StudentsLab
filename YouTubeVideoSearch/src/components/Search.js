import React, { Component } from "react";

class Search extends Component {
  state = { searchText: null };

  onChange = evt => this.setState({ searchText: evt.target.value });

  // onBlur = e => console.log(e.target.value);

  render() {
    // const { fetchYoutubeVideo } = this.props;
    //console.log(this.state.searchText);

    return (
      <input
        className="search"
        type="text"
        placeholder="Search"
        //onBlur={this.onBlur}
        value={this.state.searchText}
      ></input>
    );
  }
}

export default Search;
