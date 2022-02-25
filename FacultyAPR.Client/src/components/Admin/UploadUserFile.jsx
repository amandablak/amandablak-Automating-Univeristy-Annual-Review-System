import React, {Component} from 'react'
import Papa from 'papaparse'
import axios from 'axios'
class FileReader extends Component {
  constructor() {
    super();
    this.state = {
      CsvFile: undefined
    };
    this.updateData = this.updateData.bind(this);
  }

  handleChange = event => {
    this.setState({
      file: event.target.files[0]
    });
  };

  importCSV = () => {
    const { CsvFile } = this.state;
    var data = Papa.parse(CsvFile, {
      complete: this.updateData,
      header: true
    });
    return data
  };

  updateData(result) {
    var data = result.data;
    console.log(data);
  }


  handleSubmit = event => {
    event.preventDefault();
    const data = this.importCSV()
    axios.post(``, { data })
      .then(res => {
        console.log(res);
        console.log(res.data);
      })
  }

  
  render() {
    return (
      <div>
        <input
          className="csv-input"
          type="file"
          ref={input => {
            this.filesInput = input;
          }}
          name="file"
          placeholder={null}
          onChange={this.handleChange}
        />
        <p />
        <button type="submit" onClick={this.handleSubmit}> Upload Users</button>
      </div>
    );
  }
}

export default FileReader;
