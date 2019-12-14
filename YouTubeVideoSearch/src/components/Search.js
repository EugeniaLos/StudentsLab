import React, { Component } from "react";

class Search extends Component {
  state = { searchText: null };

  render() {
    return (
      <input
        className="search"
        type="text"
        placeholder="Search"
        value={this.state.searchText}
      ></input>
    );
  }
}

export default Search;
