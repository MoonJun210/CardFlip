# 🃏 CardFlip - Unity Mini Game 
📅 제작일: 2025.04.14

---

## 🎮 게임 개요

**CardFlip**은 일정 시간 내에 모든 짝을 맞추는 **카드 매칭 게임**입니다.  
플레이어는 화면의 16장의 카드 중 짝을 찾아 뒤집으며, **30초 안에 모든 카드 쌍을 맞추면 승리**합니다.  
카드 쌍이 일치하지 않으면 다시 뒤집히며, 끝 텍스트를 눌러 **게임을 재시작**할 수 있습니다.

---

## 🎲 게임 규칙

| 항목 | 설명 |
|------|------|
| 카드 수 | 총 16장 (8쌍) |
| 게임 시간 | 30초 |
| 목표 | 제한 시간 내 모든 카드 쌍을 맞추기 |
| 실패 조건 | 시간 초과 시 클리어 실패 |
| 성공 조건 | 시간 내 모든 카드 제거 시 클리어 |
| 재도전 | "끝" 텍스트 클릭 시 씬 재시작 |

---

## 🔁 카드 섞기 로직

- `int[] arr = {0, 0, 1, 1, ..., 7, 7}` 구성 (총 16개)
- `Random.Range(0f, 7f)`로 정렬하여 랜덤 섞기
- 4x4 위치에 카드 배치

```csharp
void Start()
{
    int[] arr = {0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7};
    arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

    for (int i = 0; i < 16; i++)
    {
        GameObject go = Instantiate(card, this.transform);
        float x = (i % 4) * 1.4f - 2.1f;
        float y = (i / 4) * 1.4f - 3.0f;
        go.transform.position = new Vector3(x, y, 0);
        go.GetComponent<Card>().Setting(arr[i]);
    }

    GameManager.instance.cardCnt = arr.Length;
}
```

## 🔍 카드 판별 요약

- 플레이어가 2장의 카드를 선택하면, GameManager에서 `Matched()` 함수로 일치 여부를 판별
- 카드의 `idx`가 같다면:
  - `DestroyCard()`를 호출하여 두 카드를 제거
  - `cardCnt`에서 2를 차감
  - 남은 카드 수가 0이 되면 게임 종료 (`Time.timeScale = 0f`)
- 카드가 다르면:
  - `CloseCard()`를 호출하여 다시 뒤집음
- 비교 후 `firstCard`, `secondCard`를 null로 초기화하여 다음 입력을 받도록 준비

---


### 💻 실제 판별 로직 (C#)

```csharp
public void Matched() 
{
    if(firstCard.idx == secondCard.idx)
    {
        // 카드 짝이 맞음 → 제거
        firstCard.DestroyCard();
        secondCard.DestroyCard();
        cardCnt -= 2;

        // 모든 카드 제거 시 게임 종료
        if(cardCnt == 0)
        {
            Time.timeScale = 0f;
            endTxt.gameObject.SetActive(true);
        }
    } 
    else
    {
        // 카드 짝이 안 맞음 → 다시 닫기
        firstCard.CloseCard();
        secondCard.CloseCard();
    }

    // 카드 비교 초기화
    firstCard = null;
    secondCard = null;
}
```
---

## 💡 향후 개선 아이디어

### 🎨 UX/UI 개선
- 카드에 **뒤집기 애니메이션** 추가
- 정답 카드에 **효과음/이펙트** 삽입
- **남은 시간 시각화** (숫자 카운트다운(긴박한 느낌의 애니메이션 추가))
- **성공/실패 시 화면 전환 연출** 강화

### 🧩 콘텐츠 확장
- **난이도 선택 기능** (카드 개수 조정: 4x4, 6x6 등)
- 다양한 **카드 테마/이미지 스킨** 제공
- **점수제 도입**: 시간, 시도 횟수 기반 점수 계산
- **턴 수 제한**을 둔 모드 추가

### 🔁 기능 추가
- 정답 카드 저장 기능 (통계 또는 학습 용도)
- **모바일 터치 대응 최적화**
- **다회 플레이 시 속도 향상** (씬 리로딩 대신 재사용 구조 고려)


