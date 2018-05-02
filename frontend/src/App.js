import React, { Component } from "react";
import "./App.css";
import Input from "../src/comp/Form";

class Stamp extends Component {
  render() {
    const { name, time } = this.props.stamp;
    return (
      <p>
        {time.toString()} bird:{" "}
        <span className="bolding">{name.toString()} </span>{" "}
      </p>
    );
  }
}

class Bird extends Component {
  state = { name: "" };
  async handleClick() {
    const { bird } = this.props;
    const body = JSON.stringify(bird);
    await fetch("http://localhost:63759/api/bird/5", { method: "PUT", body,  headers: { 'Content-Type': 'application/json'  }});
    this.props.callback();
  }
  render() {
    const { name, id, sightings } = this.props.bird;
    return (
      <tr>
        <td>{name}</td>
        <td>{sightings.length}</td>
        <td>
          <button onClick={() => this.handleClick()}>Click me</button>
        </td>
      </tr>
    );
  }
}

class App extends Component {
  state = { birds: [], stamps: [], callback: "" };
  render() {
    return (
      <div className="App">
        <header className="App-header" />

        <div className="birds">
          <br />
          <br />
          <table>
            <thead>
              <tr>
                <th>Lintulaji</th>
                <th>Lukumäärä</th>
                <th />
              </tr>
            </thead>
            <tbody>
              {this.state.birds.map(b => (
                <Bird callback={() => this.callback()} key={b.id} bird={b} />
              ))}
            </tbody>
          </table>
        </div>
        <br />
        <br />
        <div id="newbird">
          <Input callback={() => this.callback()} />
        </div>
        <div id="stamps">
          <p className="bolding">Lintuhavainnot </p>
          {this.state.stamps.map((s, i) => (
            <Stamp key={s.time.getTime()} stamp={s} />
          ))}
        </div>
      </div>
    );
  }

  async callback() {
    const result = await fetch("http://localhost:63759/api/bird", {
      method: "GET"
    });
    const data = await result.json();
     const birds = JSON.parse(data);
    const stamps = this.sortStamps(birds);
    this.setState({ birds, stamps });
  }

  async componentDidMount() {
    const result = await fetch("http://localhost:63759/api/bird", {
      method: "GET"
    });
    const data = await result.json();
    const birds=JSON.parse(data);
    const stamps = this.sortStamps(birds);
    this.setState({ birds, stamps });
  }

  sortStamps(birds) {
    const stamps = [];
    birds.forEach(bird => {
      bird.sightings.forEach(timeStampStr => {
        stamps.push({
          time: new Date(timeStampStr),
          name: bird.name
        });
      });
    });
    stamps.sort((a, b) => a.time - b.time);

    return stamps;
  }
}

export default App;
