# .Net 6.0 Web API Sample Movie Recommendation App & Swagger

This API is built using the themoviedb API to provide information about movies, lets users to write reviews on a specific movie and recommend this movie to someone else.

## Authentication

A jwt token is required to be able to use Movie Web API endpoints

Sample request to get a jwt token:

``` GET http://localhost:5125/api/Account ```

![JwtTokenPostman](https://user-images.githubusercontent.com/126048105/220542178-eb81b56c-8c24-4e3d-a6c2-27560a84e2d6.png)


## Endpoints

![Endpoints](https://user-images.githubusercontent.com/126048105/220541578-0c18fe2e-b9f6-486b-a068-bfbe25a60e52.png)


GET /api/Movies

Retrieves a list of movies, use pageSize parameter to retrieve a specific number of movies.

GET /api/Movies/{id}

Retrieves a specific movie by id.

POST /api/Movies/WriteMovieReview

Write a movie review and rate a movie from 1 to 10. 

POST /api/Movies/RecommendMovie

Recommend a movie to a specific person by using a receiver email parameter.
