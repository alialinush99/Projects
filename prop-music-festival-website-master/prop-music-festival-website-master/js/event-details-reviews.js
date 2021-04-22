$(document).ready(function() {
  getAllReviewsForCurrentEvent(getEventId());
});

function getAllReviewsForCurrentEvent(eventId) {
  var command = "getReviewsForEvent";
  $.ajax({
      type: 'Post',
      url: './db/review.php',
      data: {
        function: command,
        eventId: eventId
      },
      success: (reviews) => {
        if(reviews == "non-existent") {
          showPlaceholderForMissingReviews();
        } else {
          var reviewsParsed = JSON.parse(reviews);
          if(reviews.length > 0) {
            $("#review-container").empty();
            $.each(reviewsParsed, function(index, review) {
              addReviewToContainer(review);
            });
          }
        }
      },
      error: (result) => {
          console.log('error', result);
      }
  });
}

// Shows placeholder string when upcoming events are missing
function showPlaceholderForMissingReviews() {
  $("#review-container").append('<h4 class="placeholder-reviews">Currently the are no reviews for this event<h4>');
}

function hidePlaceholderForMissingReviews() {
  $(".placeholder-reviews").remove();
}


function starsReducer(state, action) {
    switch (action.type) {
      case 'HOVER_STAR': {
        return {
          starsHover: action.value,
          starsSet: state.starsSet
        }
      }
      case 'CLICK_STAR': {
        return {
          starsHover: state.starsHover,
          starsSet: action.value
        }
      }
        break;
      default:
        return state
    }
  }

  var StarContainer = document.getElementById('rating');
  var StarComponents = StarContainer.children;

  var state = {
    starsHover: 0,
    starsSet: 0
  }

  function render(value) {
    for(var i = 0; i < StarComponents.length; i++) {
      StarComponents[i].style.fill = i < value ? '#f39c12' : '#808080'
    }
  }

  for (var i=0; i < StarComponents.length; i++) {
    StarComponents[i].addEventListener('mouseenter', function() {
      state = starsReducer(state, {
        type: 'HOVER_STAR',
        value: this.id
      })
      render(state.starsHover);
    })

    StarComponents[i].addEventListener('click', function() {
      state = starsReducer(state, {
        type: 'CLICK_STAR',
        value: this.id
      })
      render(state.starsHover);
    })
  }

  StarContainer.addEventListener('mouseleave', function() {
    render(state.starsSet);
  })

  var review = document.getElementById('review');
  var remaining = document.getElementById('remaining');
  review.addEventListener('input', function(e) {
    review.value = (e.target.value.slice(0,999));
    remaining.innerHTML = (999-e.target.value.length);
  })

  var form = document.getElementById("review-form")

  $('#btn-submit-review').click(function(e) {
    e.preventDefault();

    $.ajax({
      type: 'Get',
      url: './db/user-data.php',
      success: (result) => {
          console.log(result);
          if (result === 'not-logged-in') {
              // Todo implement
          } else {  
            var parsedResult = JSON.parse(result);
            $.each(parsedResult, function(i, user) {

                let review = {
                  Stars: state.starsSet,
                  Review: form['review'].value,
                  EventId: getEventId(),
                  UserId: user.UserId,
                  UserName: toTitleCase(user.FirstName +  " " + user.LastName),
                  PublishDate: new Date().toISOString().slice(0, 19).replace('T', ' ')
                }
                if(review.Stars > 0) {
                  saveReviewToDB(review)
                } else {
                  alert("Please give a valid star rating when submitting a review");
                }
            });
          }
      },
      error: (result) => {
          console.log('error', result);
      }
    });  
  });

  // Get current event id from url
  function getEventId() {
    return window.location.search.split('id=')[1]
  }

  // Capitalizes first letters of each word
  const toTitleCase = (phrase) => {
    return phrase
      .toLowerCase()
      .split(' ')
      .map(word => word.charAt(0).toUpperCase() + word.slice(1))
      .join(' ');
  };

  function saveReviewToDB(review) {
    console.log(review);
    var command = "saveReview";
    $.ajax({
          type: 'POST',
          url: './db/review.php',
          data: {
            function: command,
            review: JSON.stringify(review)
          },
          success: (result) => {
              console.log(result);
              if (result === 'success') {
                  getAllReviewsForCurrentEvent(getEventId());
              }
          },
          error: (result) => {
              console.log('error', result);
          }
    });
  }

  // Add review to review container
  function addReviewToContainer(review) {
    $('#review-container').append(ReviewsContainer(review));
  }

  function ReviewsContainer(review) {
    // Creating container for review
    var container = document.createElement('div')
    container.setAttribute("id", "rc" + review.UserId);

    // Adding content
    var div = document.createElement('blockquote');
    div.className = "review";
    div.appendChild(ReviewStarContainer(review.Stars));
    div.appendChild(ReviewContentContainer(review.UserName,review.Review, review.PublishDate));
    return container.appendChild(div);
  }

  function ReviewStarContainer(stars) {
    var div = document.createElement('div');
    div.className = "stars-container";
    for (var i = 0; i < 5; i++) {
      var svg = document.createElementNS("http://www.w3.org/2000/svg", "svg");
      svg.setAttribute('viewBox',"0 12.705 512 486.59");
      svg.setAttribute('x',"0px");
      svg.setAttribute('y',"0px");
      svg.setAttribute('xml:space',"preserve");
      svg.setAttribute('class',"star");
      var svgNS = svg.namespaceURI;
      var star = document.createElementNS(svgNS,'polygon');
      star.setAttribute('points', '256.814,12.705 317.205,198.566 512.631,198.566 354.529,313.435 414.918,499.295 256.814,384.427 98.713,499.295 159.102,313.435 1,198.566 196.426,198.566');
      star.setAttribute('fill', i < stars ? '#f39c12' : '#808080');
      svg.appendChild(star);
      div.appendChild(svg);
    }
    return div;
  }

  function ReviewContentContainer(name, review, publishDate) {
    var div = document.createElement('div');
    div.className = "review-content";

    var publishDateField = document.createElement('p')
    publishDateField.innerHTML = parseStringToDate(publishDate);

    var comment = document.createElement('p');
    comment.innerHTML = review;

    var reviewee = document.createElement('div');
    reviewee.className = "reviewee footer";
    reviewee.innerHTML  = '- ' + name

    div.appendChild(publishDateField);
    div.appendChild(comment);
    div.appendChild(reviewee);

    return div;
  }

  // Parse datetime string to date
  function parseStringToDate(datetime) {
    return moment(new Date(Date.parse(datetime))).local().format("DD/MM/YYYY");
  }