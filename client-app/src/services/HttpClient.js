import axios from "axios";

class HttpClient {
  constructor(url) {
    this.url = url;
    this.baseUrl = "http://localhost:57750/api/";
    this.token =
      sessionStorage.getItem("token") || localStorage.getItem("token");
  }

  async getAll() {
    debugger;
    try {
      const response = await axios.get(this.baseUrl + this.url, {
        headers: {
          Authorization: "Bearer " + this.token,
        },
      });
      return response.data;
    } catch (error) {
      console.log(error);
    }
  }

  async getAllByPageAndQuery(parameter) {
    debugger;
    try {
      const response = await axios.get(
        this.baseUrl +
          this.url +
          "search?name=" +
          parameter.name +
          "&page=" +
          parameter.page +
          "&letter=" +
          parameter.letter,
        {
          headers: {
            Authorization: "Bearer " + this.token,
          },
        }
      );
      debugger;
      return {
        projects: response.data,
        numberOfPages: response.headers["number-of-pages"],
      };
    } catch (error) {
      console.log(error);
    }
  }

  async edit(parameter) {
    await axios
      .put(this.baseUrl + this.url, parameter, {
        headers: {
          Authorization: "Bearer " + this.token,
        },
      })
      .catch(function (error) {
        console.log(error);
      });
  }

  async delete(parameter) {
    axios
      .delete(
        this.baseUrl + this.url,
        {
          data: parameter,
        },
        {
          headers: {
            Authorization: "Bearer " + this.token,
          },
        }
      )
      .catch(function (error) {
        console.log(error);
      });
  }

  async create(parameter) {
    const response = await axios
      .post(this.baseUrl + this.url, parameter, {
        headers: {
          Authorization: "Bearer " + this.token,
        },
      })
      .catch(function (error) {
        return false;
      });
    return response.data;
  }

  async getWithQuery(query) {
    try {
      const response = await axios.get(this.baseUrl + this.url + query, {
        headers: {
          Authorization: "Bearer " + this.token,
        },
      });
      return response.data;
    } catch (error) {
      console.log(error);
    }
  }

  async download(parameter, name) {
    const response = await axios
      .post(this.baseUrl + this.url, parameter, {
        headers: {
          Authorization: "Bearer " + this.token,
        },
        responseType: "blob",
      })
      .catch(function (error) {
        return false;
      });
    debugger;
    const url = window.URL.createObjectURL(new Blob([response.data]));
    const link = document.createElement("a");
    link.href = url;
    link.setAttribute("download", name);
    document.body.appendChild(link);
    link.click();
    return response.data;
  }
}

export default HttpClient;
