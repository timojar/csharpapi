import React, { Component } from "react";

class Input extends Component {
  state = { value: "" };

  async handleClick(e) {
    const valueStr = this.state.value;
   
    const b = { id: 0, name: this.state.value, sightings:[] };
    const body = JSON.stringify(b);

     console.log(body);
    await fetch("http://localhost:63759/api/bird", { method: "POST", body, headers: { 'Content-Type': 'application/json'  } });
    this.setState({ value: "" });

    this.props.callback();
  }

  render() {
    return (
      <div>
        <label className="bolding">Uusi laji</label>
        <br />
        <input 
          value={this.state.value}
          onChange={e => {
            this.setState({ value: e.target.value });
          }}
          type="text"
        />
        <br />
        <button onClick={() => this.handleClick()}>Lisää uusi lintulaji</button>
      </div>
    );
  }
}

export default Input;
