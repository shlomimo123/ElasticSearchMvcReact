

var Animal = React.createClass({
    render: function () {
        return (
            <div>
                {this.props.details.Id}
                <br />
                {this.props.details.Gender}
                <hr />
            </div>
            )
    }
});

var AnimalsResults = React.createClass({
    render: function () {
        var Animals = this.props.results.map(function (animal) {
            return (<Animal details={animal } />)
        })
        return (
            <div>
                {Animals}
            </div>
            )
    }
});

var SearchPanel = React.createClass({
    handleSubmit: function (e) {
        e.preventDefault();
        var searchText = this.refs.searchText.value;
        axios.get('http://matrix.search.co.il/api/animals')
          .then(function (response) {
              self.setState({ animals: self.state.animals.concat(response.data) })
          })
          .catch(function (error) {
              console.log(error);
          });
        //console.log(searchText);
    },
    render: function () {
        return (
            <div>
                <form onSubmit={this.handleSubmit}>
                    <input type="search" placeholder="Search" ref="searchText"/>
                    <input type="submit" value="Search" />
                </form>
            </div>
            )
}
});

var Main = React.createClass({
    getInitialState: function () {
        return {
            animals: []
        }
    },
    componentDidMount: function () {
        var self = this;
        axios.get('http://matrix.search.co.il/api/animals')
          .then(function (response) {
              self.setState({ animals: self.state.animals.concat(response.data) })
          })
          .catch(function (error) {
              console.log(error);
          });
    },
    render: function () {
        return (
            <div>
                <SearchPanel  />
                <AnimalsResults results={this.state.animals} />
            </div>
            
            )
    }
});

ReactDOM.render(<Main />, document.getElementById("root"));







//var Animal = React.createClass({
//    render: function () {
//        return (
//            <div>
//                {this.props.details.Id}
//                <br />
//                {this.props.details.Gender}
//                <hr />
//            </div>
//            )
//    }
//});

//var AnimalsResults = React.createClass({
//    getInitialState: function () {
//        return {
//            animals: []
//        }
//    },
//    componentDidMount: function () {
//        var self = this;
//        axios.get('http://matrix.search.co.il/api/animals')
//          .then(function (response) {
//              self.setState({ animals: self.state.animals.concat(response.data) })
//          })
//          .catch(function (error) {
//              console.log(error);
//          });
//    },
//    render: function () {
//        var Animals = this.state.animals.map(function (animal) {
//            return (<Animal details={animal } />)
//        })
//        return (
//    <div>
//        {Animals}
//    </div>
//    )
//    }
//});

//var Main = React.createClass({
//    render: function () {
//        return (
//            <AnimalsResults />
//            )
//    }
//});

//ReactDOM.render(<Main />, document.getElementById("root"));