import { Component } from "react";

class Comp extends Component{
    constructor(){
        super();
        this.state={
            name: 'guest',
            count : 0
        };
    }

    increment(){
        this.setState({count: this.state.count + 1})
    }

    render(){
        return<>
        <
        </>
    }

}